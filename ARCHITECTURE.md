# Graph Database Detective - Project Structure

This project implements a demo of the Graph Database Detective using the Microsoft Agent Framework with MCP (Model Context Protocol) integration to Neo4j.

## Architecture Overview

The solution follows a pragmatic architecture with clear separation of concerns:

### 1. **Entry Point**
- **Program.cs** - Main entry point that launches `DetectiveDemo.RunAsync()`

### 2. **Configuration Layer**
- **Configuration/Neo4jConfiguration.cs** - Neo4j database configuration from environment variables
  - Loads URI, username, password, and database name
  - Provides environment variables dictionary for MCP client
  - Returns null if configuration is incomplete (fail-fast approach)

### 3. **MCP Integration Layer**
- **MCP/Neo4jMcpClientFactory.cs** - Factory for creating MCP clients
  - Creates stdio transport connection to Neo4j MCP server
  - Uses `uvx` to run `mcp-neo4j-cypher@0.5.0` package
  - Passes Neo4j configuration to the MCP server

### 4. **Agent Layer**
- **GraphDatabaseDetectiveAgent.cs** - Factory for creating the AI agent
  - Creates Azure OpenAI chat client from AIConfig
  - Configures agent with Golden Age detective personality ("Holmsworth Marot")
  - Attaches MCP tools to enable graph database queries

### 5. **AI Configuration**
- **AIConfig.cs** - Azure OpenAI configuration
  - Loads endpoint and API key from environment variables
  - Provides lazy-loaded singleton configuration
  - Uses model deployment "gpt-4o-chat"

### 6. **Demo Orchestration**
- **DetectiveDemo.cs** - Main demo orchestrator combining all components
  - Coordinates initialization, MCP connection, and agent creation
  - Manages the interactive conversation loop
  - Contains console UI helper methods in a `#region Console UI Helpers` section
  - Handles errors gracefully with detective personality
  - All simple console outputs are inline for readability
  - Complex multi-line formatted outputs use private helper methods

## File Structure

```
GraphDatabaseDetective/
??? Program.cs                              # Entry point
??? AIConfig.cs                             # Azure OpenAI configuration
??? GraphDatabaseDetectiveAgent.cs          # Agent factory
??? DetectiveDemo.cs                        # Main orchestrator + UI helpers
??? Configuration/
?   ??? Neo4jConfiguration.cs               # Neo4j config from env vars
??? MCP/
    ??? Neo4jMcpClientFactory.cs            # MCP client factory
```

## Environment Variables Required

```bash
# Neo4j Database
NEO4J_URI=neo4j+s://xxxxx.databases.neo4j.io
NEO4J_USERNAME=neo4j
NEO4J_PASSWORD=your_password
NEO4J_DATABASE=neo4j                        # Optional, defaults to "neo4j"

# Azure OpenAI
AZURE_OPENAI_ENDPOINT=https://your-endpoint.openai.azure.com/
AZURE_OPENAI_API_KEY=your_api_key
```

## Key Design Principles

1. **Pragmatic Separation of Concerns**: Each component has a clear responsibility without over-abstraction
2. **Factory Pattern**: Used for complex object creation (Agent, MCP Client)
3. **Static Classes**: Used for utilities and factories that don't maintain state
4. **Fail-Fast**: Configuration validation happens early with clear error messages
5. **Async Throughout**: All I/O operations are async for better performance
6. **Inline Simplicity**: Simple console outputs are inline; complex formatting uses helper methods
7. **Locality of Behavior**: UI helpers live in `DetectiveDemo.cs` where they're used

## Running the Demo

Simply run the project (F5 or `dotnet run`) - it will:
1. Initialize console for UTF-8 emoji support
2. Validate Neo4j and Azure OpenAI environment variables
3. Connect to Neo4j via MCP stdio transport
4. Discover available MCP tools from Neo4j server
5. Create the detective agent with MCP tools attached
6. Start an interactive conversation loop
7. Allow you to investigate crimes using natural language
8. Type 'q' or 'quit' to exit

## Detective Personality: Holmsworth Marot ???

The agent embodies traits from legendary Golden Age detectives:
- **Methodical reasoning** - Systematic analysis and pattern recognition (Poirot-style)
- **Deductive logic** - Connecting disparate facts with precision (Holmes-style)
- **Understanding of human nature** - Motives and relationships (Marple-style)

The detective uses the Neo4j graph database to:
- Uncover relationships between suspects and victims
- Analyze patterns in crime types, locations, and timeframes
- Trace paths in criminal networks
- Cross-reference multiple data points
- Present findings with clarity and detective flair

## Technology Stack

- **.NET 9** - Target framework
- **C# 13.0** - Language version
- **Microsoft.Agents.AI** - Agent framework
- **Azure.AI.OpenAI** - Azure OpenAI integration
- **ModelContextProtocol** - MCP client library
- **Neo4j** (via MCP) - Graph database for crime data
