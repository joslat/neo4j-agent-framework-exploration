// Copyright (c) 2025 Jose Luis Latorre & Zaid Zaim
//
// ✨ SPECIAL THANKS ✨
// This demo was created in collaboration with Zaid Zaim (@zaidzaim)
// We have done this together as part of a series of discussions we
// have had on MCP integration in regards to graph databases with Neo4j.
// Zaid provided invaluable support with the Neo4j MCP integration,
// environment configuration, and countless ideas.
// His expertise in graph databases and MCP helped bring this detective to life! 🕵️
//
// Collaboration highlights:
// - Neo4j crime database setup and configuration
// - MCP server identification and providing the right resources.
// - Brainstorming together on ideas and motivation for the demo and future
//   talks based on this demo
//
// Thanks Zaid! 🙌

using GraphDatabaseDetective.Configuration;
using GraphDatabaseDetective.MCP;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

namespace GraphDatabaseDetective;

/// <summary>
/// The Graph Database Detective Demo
/// 
/// Demonstrates MCP integration with a Neo4j crime database using a Golden Age detective
/// inspired by the great investigators of classic mystery fiction. This agent can query 
/// the graph database to investigate crimes, find connections between suspects, analyze 
/// patterns, and solve mysteries using the power of graph relationships!
/// 
/// The agent blends traits from legendary detectives:
/// - Methodical reasoning and attention to detail (Poirot-style)
/// - Deductive logic and pattern recognition (Holmes-style)
/// - Understanding of human nature and social connections (Marple-style)
/// 
/// The agent uses MCP to access Neo4j Cypher query capabilities dynamically.
/// 
/// Environment Variables Required:
/// - NEO4J_URI: The Neo4j database connection URI (e.g., neo4j+s://xxxxx.databases.neo4j.io)
/// - NEO4J_USERNAME: Database username (typically "neo4j")
/// - NEO4J_PASSWORD: Database password
/// - NEO4J_DATABASE: Database name (typically "neo4j")
/// - AZURE_OPENAI_ENDPOINT: Azure OpenAI endpoint
/// - AZURE_OPENAI_API_KEY: Azure OpenAI API key
/// 
/// Type 'q' or 'quit' to exit the conversation.
/// </summary>
public static class DetectiveDemo
{
    public static async Task RunAsync()
    {
        // Initialize console for UTF-8 support (emojis and special characters)
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("\n=== THE GRAPH DATABASE DETECTIVE ===\n");

        // Step 1: Load Neo4j configuration from environment variables
        Console.WriteLine("🔐 Retrieving Neo4j database credentials...");
        var neo4jConfig = Neo4jConfiguration.LoadFromEnvironment();

        if (neo4jConfig == null)
        {
            ShowNeo4jConfigurationError();
            return;
        }

        Console.WriteLine($"✅ Connected to: {neo4jConfig.Uri}");
        Console.WriteLine($"✅ Database: {neo4jConfig.Database}\n");

        // Step 2: Create MCP client connected to Neo4j
        Console.WriteLine("🔌 Initializing connection to Neo4j MCP Server...");
        await using var mcpClient = (McpClient)await Neo4jMcpClientFactory.CreateAsync(neo4jConfig);
        Console.WriteLine("✅ Connected to Neo4j Crime Database via MCP!\n");

        // Step 3: Discover available tools from MCP server
        Console.WriteLine("🔍 Discovering available database investigation tools...");
        var mcpTools = await mcpClient.ListToolsAsync().ConfigureAwait(false);
        Console.WriteLine($"✅ Holmsworth Marot has access to {mcpTools.Count} investigation tools!\n");

        // Step 4: Create the detective agent with MCP tools
        var detective = GraphDatabaseDetectiveAgent.Create([.. mcpTools.Cast<AITool>()]);

        // Step 5: Create a new conversation thread
        AgentThread thread = detective.GetNewThread();

        // Step 6: Show welcome message
        ShowWelcomeMessage();

        // Step 7: Interactive conversation loop
        await RunConversationLoopAsync(detective, thread);

        // Step 8: Show demo completion
        ShowDemoComplete();
    }

    private static async Task RunConversationLoopAsync(AIAgent detective, AgentThread thread)
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("You: ");
            Console.ResetColor();
            string? userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                continue;
            }

            // Check for quit command
            if (userInput.Equals("q", StringComparison.OrdinalIgnoreCase) ||
                userInput.Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("👋 Farewell! Thank you for this most interesting investigation.");
                Console.WriteLine("   Remember: logic and observation never fail! 🧠✨");
                Console.ResetColor();
                break;
            }

            // Get response from detective
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Holmsworth Marot 🕵️: ");
            Console.ResetColor();

            try
            {
                var response = await detective.RunAsync(userInput, thread);
                Console.WriteLine(response.Text);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }
    }

    #region Console UI Helpers

    private static void ShowNeo4jConfigurationError()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("❌ ERROR: Neo4j environment variables are not properly configured!");
        Console.WriteLine();
        Console.WriteLine("Please set the following environment variables:");
        Console.WriteLine("  • NEO4J_URI - Database connection URI");
        Console.WriteLine("  • NEO4J_USERNAME - Database username");
        Console.WriteLine("  • NEO4J_PASSWORD - Database password");
        Console.WriteLine("  • NEO4J_DATABASE - Database name (optional, defaults to 'neo4j')");
        Console.ResetColor();
    }

    private static void ShowWelcomeMessage()
    {
        Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("🎩 Good day! I am Holmsworth Marot, at your service for this investigation.");
        Console.ResetColor();
        Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════");
        Console.WriteLine();
        Console.WriteLine("My deductive faculties and investigative methods are ready to unravel any mystery.");
        Console.WriteLine("With access to the graph database, no criminal connection shall remain hidden.\n");
        Console.WriteLine("💡 I can assist you with:");
        Console.WriteLine("   • 🔍 Investigating specific crimes and their details");
        Console.WriteLine("   • 👥 Analyzing relationships between suspects and victims");
        Console.WriteLine("   • 🗺️  Exploring patterns in crime locations and types");
        Console.WriteLine("   • ⏰ Discovering temporal patterns in criminal activity");
        Console.WriteLine("   • 🕸️  Uncovering hidden connections in criminal networks");
        Console.WriteLine("   • 📊 Finding common motives and patterns across cases");
        Console.WriteLine();
        Console.WriteLine("📝 Examples of what you might ask:");
        Console.WriteLine("   • 'Show me all unsolved murders in the database'");
        Console.WriteLine("   • 'Who are the known associates of suspect John Smith?'");
        Console.WriteLine("   • 'Find all crimes that occurred near the waterfront'");
        Console.WriteLine("   • 'What connections exist between these two victims?'");
        Console.WriteLine("   • 'Show me patterns in theft crimes over the last year'");
        Console.WriteLine("   • 'Who has both a motive and opportunity for this crime?'");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Describe the case you wish to investigate, and I shall apply systematic");
        Console.WriteLine("deduction and analysis to uncover the truth!");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Type 'q' or 'quit' to conclude our investigation.\n");
        Console.WriteLine(new string('═', 80));
        Console.WriteLine();
    }

    private static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"❌ My apologies! An unexpected obstacle: {message}");
        Console.WriteLine($"💡 Perhaps we should approach this investigation from a different angle?");
        Console.WriteLine($"   Try being more specific about the crime, suspect, or relationship you seek.");
        Console.ResetColor();
        Console.WriteLine();
    }

    private static void ShowDemoComplete()
    {
        Console.WriteLine();
        Console.WriteLine("✅ Demo Complete: The investigation has concluded.");
        Console.WriteLine();
        Console.WriteLine("💡 Key Takeaways:");
        Console.WriteLine("   • MCP enables dynamic integration with Neo4j graph databases");
        Console.WriteLine("   • Graph databases excel at uncovering complex relationships");
        Console.WriteLine("   • Environment variables keep sensitive credentials secure");
        Console.WriteLine("   • AI agents can be given rich personalities that enhance user experience");
        Console.WriteLine("   • The combination of graph queries and AI reasoning creates powerful investigations!");
    }

    #endregion
}
