namespace Neo4jMarketNewsChat.Configuration;

public sealed record Neo4jConfiguration(Uri Uri, string Username, string Password, string Database)
{
    public static Neo4jConfiguration LoadFromEnvironment()
    {
        var uriText = Environment.GetEnvironmentVariable("NEO4J_URI") ?? "neo4j://localhost:7687";

        // Prefer NEO4J_USERNAME (common with Neo4j tooling), but allow NEO4J_USER (used elsewhere in this repo).
        var username = Environment.GetEnvironmentVariable("NEO4J_USERNAME")
            ?? Environment.GetEnvironmentVariable("NEO4J_USER")
            ?? "neo4j";

        // Prefer NEO4J_PASSWORD (common), but allow NEO4J_PASS (less common).
        var password = Environment.GetEnvironmentVariable("NEO4J_PASSWORD")
            ?? Environment.GetEnvironmentVariable("NEO4J_PASS")
            ?? "neo4j-password-change-me";

        var database = Environment.GetEnvironmentVariable("NEO4J_DATABASE") ?? "neo4j";

        if (!System.Uri.TryCreate(uriText, UriKind.Absolute, out var parsedUri))
        {
            throw new InvalidOperationException("Environment variable 'NEO4J_URI' must be a valid absolute URI.");
        }

        return new Neo4jConfiguration(parsedUri, username, password, database);
    }

    public IDictionary<string, string?> GetEnvironmentVariablesForMcp()
    {
        // mcp-neo4j-cypher expects these names.
        return new Dictionary<string, string?>(StringComparer.Ordinal)
        {
            ["NEO4J_URI"] = Uri.ToString(),
            ["NEO4J_USERNAME"] = Username,
            ["NEO4J_PASSWORD"] = Password,
            ["NEO4J_DATABASE"] = Database
        };
    }
}
