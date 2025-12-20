using ModelContextProtocol;
using ModelContextProtocol.Client;
using Neo4jMarketNewsChat.Configuration;

namespace Neo4jMarketNewsChat.MCP;

/// <summary>
/// Factory for creating MCP clients connected to Neo4j using stdio transport.
/// </summary>
public static class Neo4jMcpClientFactory
{
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
            Name = "FinancialNewsSpreadGraph",
            Command = uvxPath,
            // Use latest 0.5.2 which fixed FastMCP 3.x compatibility (removed dependencies arg)
            Arguments = ["mcp-neo4j-cypher@0.5.2", "--transport", "stdio"],
            EnvironmentVariables = config.GetEnvironmentVariablesForMcp()
        });

        return await McpClientFactory.CreateAsync(transport);
    }
}
