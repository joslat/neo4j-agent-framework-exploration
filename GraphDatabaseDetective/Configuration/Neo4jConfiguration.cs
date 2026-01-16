// Copyright (c) 2025 Jose Luis Latorre & Zaid Zaim
//
// ? SPECIAL THANKS ?
// This demo was created in collaboration with Zaid Zaim (@zaidzaim)
// We have done this together as part of a series of discussions we
// have had on MCP integration in regards to graph databases with Neo4j.
// Zaid provided invaluable support with the Neo4j MCP integration,
// environment configuration, and countless ideas.
// His expertise in graph databases and MCP helped bring this detective to life! ???
//
// Collaboration highlights:
// - Neo4j crime database setup and configuration
// - MCP server identification and providing the right resources.
// - Brainstorming together on ideas and motivation for the demo and future
//   talks based on this demo
//
// Thanks Zaid! ??

namespace GraphDatabaseDetective.Configuration;

/// <summary>
/// Configuration for Neo4j database connection.
/// Retrieves credentials from environment variables.
/// </summary>
public class Neo4jConfiguration
{
    public string Uri { get; }
    public string Username { get; }
    public string Password { get; }
    public string Database { get; }

    private Neo4jConfiguration(string uri, string username, string password, string database)
    {
        Uri = uri;
        Username = username;
        Password = password;
        Database = database;
    }

    /// <summary>
    /// Loads Neo4j configuration from environment variables.
    /// </summary>
    /// <returns>Neo4j configuration if all required variables are set; otherwise null.</returns>
    public static Neo4jConfiguration? LoadFromEnvironment()
    {
        var uri = Environment.GetEnvironmentVariable("NEO4J_URI");
        var username = Environment.GetEnvironmentVariable("NEO4J_USERNAME");
        var password = Environment.GetEnvironmentVariable("NEO4J_PASSWORD");
        var database = Environment.GetEnvironmentVariable("NEO4J_DATABASE") ?? "neo4j";

        if (string.IsNullOrWhiteSpace(uri) ||
            string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        return new Neo4jConfiguration(uri, username, password, database);
    }

    /// <summary>
    /// Gets environment variables dictionary for MCP client.
    /// mcp-neo4j-cypher expects these specific variable names.
    /// </summary>
    public IDictionary<string, string?> GetEnvironmentVariablesForMcp()
    {
        return new Dictionary<string, string?>(StringComparer.Ordinal)
        {
            ["NEO4J_URI"] = Uri,
            ["NEO4J_USERNAME"] = Username,
            ["NEO4J_PASSWORD"] = Password,
            ["NEO4J_DATABASE"] = Database
        };
    }
}
