using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
using Neo4jMarketNewsChat.Configuration;
using Neo4jMarketNewsChat.MCP;

namespace Neo4jMarketNewsChat;

public static class MarketNewsChatDemo
{
    public static async Task RunAsync()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine();
        Console.WriteLine("=== MARKET NEWS GRAPH CHAT ===");
        Console.WriteLine();

        Console.WriteLine("Retrieving Neo4j database credentials...");
        var neo4jConfig = Neo4jConfiguration.LoadFromEnvironment();

        Console.WriteLine($"Connected to: {neo4jConfig.Uri}");
        Console.WriteLine($"Database: {neo4jConfig.Database}");
        Console.WriteLine();

        Console.WriteLine("Initializing connection to Neo4j MCP server...");

        IMcpClient mcpClient;
        try
        {
            mcpClient = await Neo4jMcpClientFactory.CreateAsync(neo4jConfig);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Failed to start MCP client: {ex.Message}");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Notes:");
            Console.WriteLine("  - This demo uses 'uvx' to run the Neo4j MCP server (mcp-neo4j-cypher). Ensure uv/uvx is installed.");
            Console.WriteLine("  - Neo4j must be running and reachable (NEO4J_URI).\n");
            return;
        }

        await using (mcpClient)
        {
            Console.WriteLine("Connected to Neo4j via MCP.");
            Console.WriteLine();

            Console.WriteLine("Discovering available MCP tools...");
            var mcpTools = await mcpClient.ListToolsAsync().ConfigureAwait(false);
            Console.WriteLine($"Agent has access to {mcpTools.Count} tools.");
            Console.WriteLine();

            AIAgent agent;
            try
            {
                agent = MarketNewsGraphAgent.Create([.. mcpTools.Cast<AITool>()]);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed to create Azure OpenAI agent: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Required environment variables:");
                Console.WriteLine("  - AZURE_OPENAI_ENDPOINT");
                Console.WriteLine("  - AZURE_OPENAI_API_KEY");
                Console.WriteLine("Optional:");
                Console.WriteLine("  - AZURE_OPENAI_DEPLOYMENT (defaults to 'gpt-4o')");
                Console.WriteLine();
                return;
            }

            AgentThread thread = agent.GetNewThread();

            ShowWelcomeMessage();
            await RunConversationLoopAsync(agent, thread);

            Console.WriteLine();
            Console.WriteLine("Chat ended.");
        }
    }

    private static async Task RunConversationLoopAsync(AIAgent agent, AgentThread thread)
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("You: ");
            Console.ResetColor();

            var userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                continue;
            }

            if (userInput.Equals("q", StringComparison.OrdinalIgnoreCase) ||
                userInput.Equals("quit", StringComparison.OrdinalIgnoreCase) ||
                userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Agent: ");
            Console.ResetColor();

            try
            {
                var response = await agent.RunAsync(userInput, thread);
                Console.WriteLine(response.Text);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }
    }

    private static void ShowNeo4jConfigurationError()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ERROR: Neo4j environment variables are not properly configured.");
        Console.WriteLine();
        Console.WriteLine("Please set:");
        Console.WriteLine("  - NEO4J_URI (e.g. neo4j://localhost:7687)");
        Console.WriteLine("  - NEO4J_USERNAME (or NEO4J_USER)");
        Console.WriteLine("  - NEO4J_PASSWORD");
        Console.WriteLine("  - NEO4J_DATABASE (optional; default 'neo4j')");
        Console.ResetColor();
        Console.WriteLine();
    }

    private static void ShowWelcomeMessage()
    {
        Console.WriteLine(new string('=', 80));
        Console.WriteLine("Ask about news events, companies, suppliers, partners, or contagion paths.");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  - What companies are impacted by N-2025-12-18-001?");
        Console.WriteLine("  - Show me NVDA's suppliers/customers and key partners.");
        Console.WriteLine("  - What are second-order effects of the policy shock event?");
        Console.WriteLine("  - Find recent negative news and who it affects.");
        Console.WriteLine();
        Console.WriteLine("Type 'q' to quit.");
        Console.WriteLine(new string('=', 80));
        Console.WriteLine();
    }

    private static void ShowError(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {ex.Message}");
        Console.ResetColor();
        Console.WriteLine();
    }
}
