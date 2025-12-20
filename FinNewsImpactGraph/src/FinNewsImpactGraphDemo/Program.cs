using System.Text.Json;
using System.Linq;
using Neo4j.Driver;
using System.Text;

internal static class Program
{
	private const string DefaultNewsId = "N-2025-12-18-001";

	private const string SeedCheckQuery = @"
MATCH (c:Company {ticker: 'NVDA'})
RETURN count(c) AS cnt
";

	private static readonly IReadOnlyList<string> FallbackConstraints =
	[
		"CREATE CONSTRAINT company_ticker IF NOT EXISTS FOR (c:Company) REQUIRE c.ticker IS UNIQUE",
		"CREATE CONSTRAINT country_code IF NOT EXISTS FOR (c:Country) REQUIRE c.code IS UNIQUE",
		"CREATE CONSTRAINT market_code IF NOT EXISTS FOR (m:Market) REQUIRE m.code IS UNIQUE",
		"CREATE CONSTRAINT sector_name IF NOT EXISTS FOR (s:Sector) REQUIRE s.name IS UNIQUE",
		"CREATE CONSTRAINT news_id IF NOT EXISTS FOR (n:NewsEvent) REQUIRE n.id IS UNIQUE",
	];

	private const string ImpactQuery = @"
MATCH (n:NewsEvent {id: $newsId})-[a:AFFECTS]->(c0:Company)
WITH n, c0, coalesce(a.directImpact, 1.0) AS directImpact, coalesce(n.sentiment, 0.0) AS sentiment
// Walk 0..2 hops from the directly affected company through ownership/partnership/supply links.
MATCH p=(c0)-[r:OWNS|PARTNERS_WITH|SUPPLIES_TO*0..2]-(c:Company)
WITH c0, c, directImpact, sentiment, relationships(p) AS rels
// For this demo, ignore paths that are only ownership edges (they tend to be less intuitive
// for explaining AI supply-chain/partner propagation). Still include the 0-hop case.
WHERE size(rels) = 0 OR any(rel IN rels WHERE type(rel) <> 'OWNS')
WITH
  c0,
  c,
  directImpact,
  sentiment,
	size(rels) AS hops,
	reduce(pathWeight = 1.0, rel IN rels |
	pathWeight
	* (case type(rel)
		when 'SUPPLIES_TO' then 1.0
		when 'PARTNERS_WITH' then 0.85
		when 'OWNS' then 0.5
		else 0.75
	  end)
	* coalesce(
	  rel.pct / 100.0,
	  rel.strength,
	  rel.criticality,
	  0.5
	)
	) AS pathWeight
WITH
  c0,
  c,
  // A simple risk score: (negative sentiment only) * directImpact * pathWeight
	// plus a small hop-distance decay to keep results intuitive.
	(case when sentiment < 0 then -sentiment else 0 end)
	* directImpact
	* pathWeight
	* (case hops when 0 then 1.0 else (0.85 ^ hops) end) AS risk
RETURN
  c0.ticker AS sourceTicker,
  c.ticker AS ticker,
  c.name AS name,
  round(sum(risk) * 1000) / 1000.0 AS riskScore
ORDER BY riskScore DESC, ticker ASC
LIMIT 20
";

	private const string StatsQuery = @"
MATCH (n) WITH count(n) AS nodes
MATCH ()-[r]->() WITH nodes, count(r) AS rels
RETURN nodes, rels
";

	public static async Task Main()
	{
		var config = AppConfig.LoadFromEnvironment();
		var audit = new QueryAudit(Path.Combine(Environment.CurrentDirectory, "query-audit.jsonl"));

		Console.WriteLine("Neo4j finance graph demo");
		Console.WriteLine($"URI: {config.Uri}");

		using var driver = GraphDatabase.Driver(
			config.Uri,
			AuthTokens.Basic(config.User, config.Password),
			o =>
			{
				o.WithMaxConnectionPoolSize(50);
				o.WithConnectionAcquisitionTimeout(TimeSpan.FromSeconds(15));
			});

		await driver.VerifyConnectivityAsync();
		Console.WriteLine("Connected.");

		await EnsureSeedDataAsync(driver, audit);

		var newsId = config.NewsId ?? DefaultNewsId;
		Console.WriteLine($"\nAnalyzing propagation from NewsEvent: {newsId}");

		var results = await RunAsync(driver, audit, ImpactQuery, new { newsId });
		foreach (var row in results)
		{
			Console.WriteLine($"{Str(row, "ticker"),-6}  risk={Num(row, "riskScore"),5}  source={Str(row, "sourceTicker"),-6}  {Str(row, "name")}");
		}

		Console.WriteLine("\nQuick stats:");
		var stats = await RunAsync(driver, audit, StatsQuery, new { });
		if (stats.Count > 0)
		{
			Console.WriteLine($"nodes={Num(stats[0], "nodes")} rels={Num(stats[0], "rels")}");
		}

		Console.WriteLine("\nDone. Query audit written to query-audit.jsonl");
	}

	private static async Task EnsureSeedDataAsync(IDriver driver, QueryAudit audit)
	{
		var seed = TryLoadSeedStatements(out var seedPath)
			?? (constraints: FallbackConstraints, data: Array.Empty<string>());

		foreach (var query in seed.constraints)
		{
			await RunAsync(driver, audit, query, new { });
		}

		if (await IsSeedPresentAsync(driver, audit))
		{
			Console.WriteLine("Seed data already present; skipping seed.");
			return;
		}

		if (seed.data.Count == 0)
		{
			Console.WriteLine("No seed data statements found; skipping seed.");
			return;
		}

		Console.WriteLine(seedPath is null
			? "Seeding sample graph data..."
			: $"Seeding sample graph data from: {seedPath}");

		foreach (var statement in seed.data)
		{
			await RunAsync(driver, audit, statement, new { });
		}
	}

	private static (IReadOnlyList<string> constraints, IReadOnlyList<string> data)? TryLoadSeedStatements(out string? pathUsed)
	{
		pathUsed = null;
		var candidates = new List<string>
		{
			Path.Combine(AppContext.BaseDirectory, "seed.cypher"),
			Path.Combine(Environment.CurrentDirectory, "neo4j", "import", "seed.cypher"),
		};

		foreach (var path in candidates)
		{
			if (!File.Exists(path))
			{
				continue;
			}

			var script = File.ReadAllText(path);
			var statements = ParseCypherStatements(script);
			var constraints = statements
				.Where(s => s.TrimStart().StartsWith("CREATE CONSTRAINT", StringComparison.OrdinalIgnoreCase))
				.ToArray();
			var data = statements
				.Where(s => !s.TrimStart().StartsWith("CREATE CONSTRAINT", StringComparison.OrdinalIgnoreCase))
				.ToArray();

			pathUsed = path;
			return (constraints.Length == 0 ? FallbackConstraints : constraints, data);
		}

		return null;
	}

	private static IReadOnlyList<string> ParseCypherStatements(string script)
	{
		// Neo4j Browser supports :source and multi-statement scripts.
		// Bolt RUN expects one statement at a time, so we split the script into statements.
		var normalized = new StringBuilder();
		using var reader = new StringReader(script);
		string? line;
		while ((line = reader.ReadLine()) is not null)
		{
			var trimmed = line.TrimStart();
			if (trimmed.StartsWith("//", StringComparison.Ordinal))
			{
				continue;
			}

			normalized.AppendLine(line);
		}

		var statements = new List<string>();
		var current = new StringBuilder();
		var inSingleQuote = false;
		var text = normalized.ToString();

		for (var i = 0; i < text.Length; i++)
		{
			var ch = text[i];

			if (ch == '\'' )
			{
				// Cypher escapes a single quote inside a single-quoted string as two single quotes: ''
				if (i + 1 < text.Length && text[i + 1] == '\'')
				{
					current.Append("''");
					i++;
					continue;
				}

				inSingleQuote = !inSingleQuote;
				current.Append(ch);
				continue;
			}

			if (ch == ';' && !inSingleQuote)
			{
				var statement = current.ToString().Trim();
				if (statement.Length > 0)
				{
					statements.Add(statement);
				}

				current.Clear();
				continue;
			}

			current.Append(ch);
		}

		var last = current.ToString().Trim();
		if (last.Length > 0)
		{
			statements.Add(last);
		}

		return statements;
	}

	private static async Task<bool> IsSeedPresentAsync(IDriver driver, QueryAudit audit)
	{
		var rows = await RunAsync(driver, audit, SeedCheckQuery, new { });
		if (rows.Count == 0)
		{
			return false;
		}

		if (!rows[0].TryGetValue("cnt", out var value))
		{
			return false;
		}

		return ToLong(value) > 0;
	}

	private static long ToLong(object? value)
		=> value switch
		{
			null => 0,
			int i => i,
			long l => l,
			double d => (long)d,
			decimal m => (long)m,
			_ => long.TryParse(value.ToString(), out var parsed) ? parsed : 0,
		};

	private static async Task<IReadOnlyList<IReadOnlyDictionary<string, object>>> RunAsync(
		IDriver driver,
		QueryAudit audit,
		string cypher,
		object parameters)
	{
		var startedAt = DateTimeOffset.UtcNow;
		try
		{
			await audit.AppendAsync(new QueryAuditEntry(startedAt, cypher, parameters, "start", null));

			await using var session = driver.AsyncSession(o => o.WithDefaultAccessMode(AccessMode.Write));
			return await session.ExecuteWriteAsync(async tx =>
			{
				var cursor = await tx.RunAsync(cypher, parameters);
				return await cursor.ToListAsync(r => (IReadOnlyDictionary<string, object>)r.Keys.ToDictionary(k => k, k => r[k]));
			});
		}
		catch (Exception ex)
		{
			await audit.AppendAsync(new QueryAuditEntry(startedAt, cypher, parameters, "error", ex.Message));
			throw;
		}
	}

	private static string Str(IReadOnlyDictionary<string, object> row, string key)
		=> row.TryGetValue(key, out var value) ? value?.ToString() ?? "" : "";

	private static string Num(IReadOnlyDictionary<string, object> row, string key)
	{
		if (!row.TryGetValue(key, out var value) || value is null)
		{
			return "0";
		}

		return value switch
		{
			int i => i.ToString(),
			long l => l.ToString(),
			double d => d.ToString("0.###"),
			float f => f.ToString("0.###"),
			decimal m => m.ToString("0.###"),
			_ => value.ToString() ?? "0"
		};
	}

	private sealed record AppConfig(Uri Uri, string User, string Password, string? NewsId)
	{
		public static AppConfig LoadFromEnvironment()
		{
			var uriText = Environment.GetEnvironmentVariable("NEO4J_URI") ?? "neo4j://localhost:7687";
			var user = Environment.GetEnvironmentVariable("NEO4J_USER") ?? "neo4j";
			var password = Environment.GetEnvironmentVariable("NEO4J_PASSWORD") ?? "neo4j-password-change-me";
			var newsId = Environment.GetEnvironmentVariable("NEWS_ID");
			return new AppConfig(new Uri(uriText), user, password, newsId);
		}
	}

	private sealed record QueryAuditEntry(DateTimeOffset Timestamp, string Query, object Parameters, string Status, string? Error);

	private sealed class QueryAudit
	{
		private readonly string _path;
		private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = false };

		public QueryAudit(string path) => _path = path;

		public Task AppendAsync(QueryAuditEntry entry)
		{
			var json = JsonSerializer.Serialize(entry, _jsonOptions);
			return File.AppendAllTextAsync(_path, json + Environment.NewLine);
		}
	}
}



