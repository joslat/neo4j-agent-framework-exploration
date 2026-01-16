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
        // Find uvx executable - check multiple possible locations
        var uvxPath = FindUvxExecutable();

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

    /// <summary>
    /// Finds the uvx executable by checking multiple possible installation locations.
    /// </summary>
    /// <returns>Path to uvx executable or "uvx" to use PATH lookup</returns>
    private static string FindUvxExecutable()
    {
        // List of potential paths to check for uvx
        var potentialPaths = new List<string>();

        // Check WinGet packages folder (if it exists)
        var wingetPackagesPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Microsoft", "WinGet", "Packages");
        
        if (Directory.Exists(wingetPackagesPath))
        {
            try
            {
                var wingetUvx = Directory.GetFiles(wingetPackagesPath, "uvx.exe", SearchOption.AllDirectories)
                    .FirstOrDefault();
                if (!string.IsNullOrEmpty(wingetUvx))
                    return wingetUvx;
            }
            catch (Exception)
            {
                // Ignore errors when searching WinGet folder
            }
        }

        // Check common installation paths
        var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        
        potentialPaths.AddRange([
            // uv/uvx installed via pip or standalone installer
            Path.Combine(userProfile, ".local", "bin", "uvx.exe"),
            Path.Combine(userProfile, ".local", "bin", "uvx"),
            // Cargo installation
            Path.Combine(userProfile, ".cargo", "bin", "uvx.exe"),
            // Scoop installation
            Path.Combine(userProfile, "scoop", "shims", "uvx.exe"),
            // Python Scripts folder
            Path.Combine(userProfile, "AppData", "Local", "Programs", "Python", "Python312", "Scripts", "uvx.exe"),
            Path.Combine(userProfile, "AppData", "Local", "Programs", "Python", "Python311", "Scripts", "uvx.exe"),
            Path.Combine(userProfile, "AppData", "Roaming", "Python", "Python312", "Scripts", "uvx.exe"),
            Path.Combine(userProfile, "AppData", "Roaming", "Python", "Python311", "Scripts", "uvx.exe"),
        ]);

        foreach (var path in potentialPaths)
        {
            if (File.Exists(path))
                return path;
        }

        // Fall back to PATH lookup - let the system find uvx
        return "uvx";
    }
}
