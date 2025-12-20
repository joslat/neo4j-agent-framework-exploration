# Finance graph demo (Neo4j Community + .NET 10)

Architecture + runbook: [ARCHITECTURE.md](ARCHITECTURE.md)

Schema reference: [GraphDbSchema.md](GraphDbSchema.md)

This repo is a small “financial contagion” prototype: a Neo4j graph + a .NET console app that seeds a sample dataset and runs a simple risk-propagation query from a news event.

## Start Neo4j

```powershell
cd C:\Users\josla\finance-neo4j
docker compose up -d
```

Neo4j Browser:
- http://localhost:7474
- Login: `neo4j` / `neo4j-password-change-me` (change it in `docker-compose.yml`)

Bolt (drivers):
- `neo4j://localhost:7687`

## Seed data (optional)

You can seed the sample dataset in **either** of these ways:

1) **Automatic seed (recommended):** the .NET app seeds on startup if it detects the graph is not present.

	- The app loads and executes the same `neo4j/import/seed.cypher` script (copied into the app output at build time), so there’s a single source of truth for the sample dataset.

2) **Manual seed (recommended for “see what’s happening”):** run the Cypher file in Neo4j Browser.

Both seeding approaches are safe to re-run (constraint creation is `IF NOT EXISTS`, and data uses `MERGE`).

Open Neo4j Browser and run:

```cypher
:source /import/seed.cypher
```

(Or copy/paste `neo4j/import/seed.cypher` into the Browser.)

## Run the C# app

```powershell
cd C:\Users\josla\finance-neo4j
# Optional: set env vars if you changed credentials
$env:NEO4J_URI = "neo4j://localhost:7687"
$env:NEO4J_USER = "neo4j"
$env:NEO4J_PASSWORD = "neo4j-password-change-me"

# Optional: select which NewsEvent to analyze
# $env:NEWS_ID = "N-2025-12-18-001" # negative news (default)
# $env:NEWS_ID = "N-2025-12-18-003" # positive-ish enterprise AI adoption story
# $env:NEWS_ID = "N-2025-12-18-005" # synthetic policy shock (demo)

dotnet run --project .\src\FinNewsImpactGraphDemo\FinNewsImpactGraphDemo.csproj
```

## Run the chat app (Azure OpenAI + MCP)

This second console app connects to the same Neo4j graph via an MCP tool (Cypher) and lets you chat with the dataset.

Prereqs:
- Neo4j running (see above)
- `uvx` available on PATH (from Astral `uv`) so the app can launch `mcp-neo4j-cypher` over stdio
- Azure OpenAI endpoint + key configured

```powershell
cd C:\Users\josla\finance-neo4j

# Neo4j defaults (override if needed)
if (-not $env:NEO4J_URI) { $env:NEO4J_URI = "neo4j://localhost:7687" }
if (-not $env:NEO4J_USER) { $env:NEO4J_USER = "neo4j" }
if (-not $env:NEO4J_PASSWORD) { $env:NEO4J_PASSWORD = "neo4j-password-change-me" }

# Azure OpenAI
$env:AZURE_OPENAI_ENDPOINT = "https://<your-resource>.openai.azure.com/"
$env:AZURE_OPENAI_API_KEY = "<your-key>"
# Optional: deployment name (defaults to gpt-4o)
# $env:AZURE_OPENAI_DEPLOYMENT = "gpt-4o"

dotnet run --project .\src\FinNewsImpactGraphAgentChat\FinNewsImpactGraphAgentChat.csproj
```

Expected outcome:

- Connects to Neo4j and verifies connectivity
- Seeds the graph only if missing (otherwise prints “Seed data already present; skipping seed.”)
- Prints a ranked list of companies with `riskScore` based on the selected `NEWS_ID`
- Prints `nodes=<n> rels=<m>` quick stats
- Writes a local query audit log to `query-audit.jsonl`

## Notes on logs/metrics (Community)

Neo4j Community includes Browser visualization and standard logs.
Some deeper server-side query logging and metrics features are Enterprise-focused in Neo4j 5+.
This demo logs all app queries client-side and includes Cypher "health/metrics" queries you can run interactively.

## Neo4j Browser walkthrough (guided)

Open Neo4j Browser at `http://localhost:7474`, login, then run these in order.

If you see empty results but you expected data, make sure you’re in the right database:

```cypher
:use neo4j
```

### 1) Verify the dataset exists

Graph view (returns nodes):

```cypher
MATCH (c:Company)
RETURN c
ORDER BY c.ticker
LIMIT 50;
```

Table view (returns properties only):

```cypher
MATCH (c:Company)
RETURN c.ticker AS ticker, c.name AS name
ORDER BY ticker;
```

### 2) See the schema (labels + relationship types)

Neo4j will render a schema visualization for the DB:

```cypher
CALL db.schema.visualization();
```

Also useful:

```cypher
SHOW CONSTRAINTS;
```

### 3) Inspect the news events

Graph view (returns the event and what it affects):

```cypher
MATCH (n:NewsEvent)-[a:AFFECTS]->(c:Company)
RETURN n, a, c
ORDER BY n.id;
```

Table view (returns properties only):

```cypher
MATCH (n:NewsEvent)
RETURN n.id AS id, n.source AS source, n.headline AS headline, n.summary AS summary,
	   n.sentiment AS sentiment, n.ts AS ts, n.url AS url
ORDER BY id;
```

Graph view (market association tags):

```cypher
MATCH (n:NewsEvent)-[r:ASSOCIATED_WITH]->(m:Market)
RETURN n, r, m
ORDER BY n.id;
```

### 4) Inspect the key relationships behind propagation

Graph view (returns nodes + relationships):

```cypher
MATCH (a:Company)-[r:OWNS|PARTNERS_WITH|SUPPLIES_TO]->(b:Company)
RETURN a, r, b;
```

Table view (returns properties only):

```cypher
MATCH (a:Company)-[r:OWNS|PARTNERS_WITH|SUPPLIES_TO]->(b:Company)
RETURN a.ticker AS from, type(r) AS rel, b.ticker AS to,
	   r.pct AS pct, r.strength AS strength, r.criticality AS criticality
ORDER BY from, rel, to;
```

Why you sometimes only see a table (and no graph):

- Neo4j Browser can only render **Graph** mode when your `RETURN` includes nodes/relationships/paths (e.g., `RETURN a, r, b` or `RETURN p`).
- If you only `RETURN` scalars/properties (e.g., `RETURN c.name`), Browser will show a table/text because there are no graph objects to draw.
- After running a query, use the result pane’s view toggle (Table / Graph / Text) and choose **Graph** when applicable.

## Visualization demo (Neo4j Browser)

This is the “graph view” demo. The goal is to show the neighborhood that the analytics query will score.

### A) Visualize the propagation neighborhood from a news event

This returns paths; in Neo4j Browser switch the result view to **Graph**.

```cypher
MATCH (n:NewsEvent {id:'N-2025-12-18-001'})-[a:AFFECTS]->(c0:Company)
MATCH p=(c0)-[:OWNS|PARTNERS_WITH|SUPPLIES_TO*0..2]-(c:Company)
RETURN n, a, p;
```

### B) Visualize a simpler “centered” neighborhood (starting at VOLT)

```cypher
MATCH p=(c0:Company {ticker:'NVDA'})-[:OWNS|PARTNERS_WITH|SUPPLIES_TO*0..2]-(c:Company)
RETURN p;
```

## Demo script (end-to-end)

Use this as a guided, repeatable demonstration.

### 0) Optional: start from a clean slate

- Stop Neo4j: `docker compose down`
- Delete `neo4j/data` to wipe the DB
- Start again: `docker compose up -d`

### 1) Bring up Neo4j and confirm access

- Browser: `http://localhost:7474`
- Bolt: `neo4j://localhost:7687`

### 2) Neo4j Browser walkthrough (schema + data)

Run the walkthrough queries from the “Neo4j Browser walkthrough (guided)” section above:

- `:use neo4j`
- `CALL db.schema.visualization();`
- list companies (Graph view query)
- list news events (Graph view query)
- list OWNS/PARTNERS_WITH/SUPPLIES_TO edges (Graph view query)

If the graph is empty, either:

- run `:source /import/seed.cypher`, or
- run the .NET app once (it seeds if missing)

### 3) Visualization demo (show the structure)

Run the “Visualization demo (Neo4j Browser)” query and narrate:

- “This news event directly affects one company.”
- “We walk 0..2 hops over ownership/partnership/supply-chain links.”
- “Those edges have weights (`pct`, `strength`, `criticality`) used to compute a path weight.”

### 4) Analytics demo (run the .NET app)

From PowerShell (repo root):

```powershell
$env:NEO4J_URI = "neo4j://localhost:7687"
$env:NEO4J_USER = "neo4j"
$env:NEO4J_PASSWORD = "neo4j-password-change-me"

# Negative news: should show non-zero risk propagation
$env:NEWS_ID = "N-2025-12-18-001"
dotnet run --project .\src\FinNewsImpactGraphDemo\FinNewsImpactGraphDemo.csproj
```

Talk track:

- “The app calculates a simple risk score from negative sentiment only.”
- “Direct impact comes from the AFFECTS relationship.”
- “Indirect impact flows through the graph, using relationship weights and a hop-distance decay.”
- “For demo clarity, we ignore paths that are only ownership edges; supply/partnership paths are more intuitive for AI ecosystem propagation.”

Now contrast with a positive event:

```powershell
$env:NEWS_ID = "N-2025-12-18-002"
dotnet run --project .\src\FinNewsImpactGraphDemo\FinNewsImpactGraphDemo.csproj
```

Expected: near-zero risk scores (the demo model ignores positive sentiment in the risk calculation).

You can also try a supply-side shock:

```powershell
$env:NEWS_ID = "N-2025-12-18-005"
dotnet run --project .\src\FinNewsImpactGraphDemo\FinNewsImpactGraphDemo.csproj
```

Note: `N-2025-12-18-005` is intentionally synthetic (demo-only) and not meant to represent a factual news claim.

### 5) What-if tweaks (make the demo interactive)

Make one change in Neo4j Browser, then rerun the app to show the ranking shifts.

**A) Make the supply-chain less critical**

```cypher
MATCH (:Company {ticker:'TSMC'})-[r:SUPPLIES_TO]->(:Company {ticker:'NVDA'})
SET r.criticality = 0.2
RETURN r;
```

**B) Increase ownership exposure**

```cypher
MATCH (:Company {ticker:'MSFT'})-[r:OWNS]->(:Company {ticker:'GITHUB'})
SET r.pct = 100.0
RETURN r;
```

**C) Add a new relationship to create an additional propagation channel**

```cypher
MATCH (a:Company {ticker:'NVDA'}), (b:Company {ticker:'OPENAI'})
MERGE (a)-[r:SUPPLIES_TO]->(b)
SET r.criticality = 0.3
RETURN r;
```

After each change:

- rerun `dotnet run` with `NEWS_ID = "N-2025-12-18-001"`
- optionally rerun the visualization query to show the new path(s)
