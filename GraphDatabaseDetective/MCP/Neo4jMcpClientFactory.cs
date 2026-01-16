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

using GraphDatabaseDetective.Configuration;
using ModelContextProtocol;
using ModelContextProtocol.Client;

namespace GraphDatabaseDetective.MCP;

/// <summary>
/// Factory for creating MCP clients connected to Neo4j database using stdio transport.
/// </summary>
public static class Neo4jMcpClientFactory
{
    /// <summary>
    /// Creates an MCP client connected to Neo4j database using stdio transport.
    /// </summary>
    /// <param name="config">Neo4j configuration</param>
    /// <returns>Connected MCP client</returns>
    public static async Task<IMcpClient> CreateAsync(Neo4jConfiguration config)
    {
        // Find uvx in WinGet packages folder
        var uvxPath = Directory.GetFiles(
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                "Microsoft", "WinGet", "Packages"),
            "uvx.exe",
            SearchOption.AllDirectories).FirstOrDefault() ?? "uvx";

        var transport = new StdioClientTransport(new()
        {
            Name = "Neo4jCrimeDatabaseMCP",
            Command = uvxPath,
            // Use latest 0.5.2 which fixed FastMCP 3.x compatibility (removed dependencies arg)
            Arguments = ["mcp-neo4j-cypher@0.5.2", "--transport", "stdio"],
            EnvironmentVariables = config.GetEnvironmentVariablesForMcp()
        });

        return await McpClientFactory.CreateAsync(transport);
    }
}
