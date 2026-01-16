# GraphDatabaseDetective - Complete Guide

## Overview

The GraphDatabaseDetective is an interactive AI-powered crime investigation agent that leverages Neo4j graph database capabilities through MCP (Model Context Protocol). The agent, named "Holmsworth Marot," combines classic detective reasoning with modern graph database queries to uncover relationships, patterns, and connections in crime data.

## 🎭 The Detective: Holmsworth Marot

The agent embodies traits from legendary Golden Age detectives:

| Inspiration | Trait | Application |
|-------------|-------|-------------|
| Hercule Poirot | Methodical reasoning | Systematic analysis and attention to detail |
| Sherlock Holmes | Deductive logic | Connecting disparate facts with precision |
| Miss Marple | Human nature insight | Understanding motives and relationships |

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                        User Interface                            │
│                    (Console Application)                         │
└─────────────────────────┬───────────────────────────────────────┘
                          │
                          ▼
┌─────────────────────────────────────────────────────────────────┐
│                     DetectiveDemo.cs                             │
│              (Orchestration & Conversation Loop)                 │
└─────────────────────────┬───────────────────────────────────────┘
                          │
                          ▼
┌─────────────────────────────────────────────────────────────────┐
│               GraphDatabaseDetectiveAgent.cs                     │
│            (AI Agent with Detective Personality)                 │
│                                                                  │
│  ┌─────────────────┐    ┌──────────────────────────────────┐   │
│  │  Azure OpenAI   │◄───│  Microsoft.Agents.AI Framework   │   │
│  │   (gpt-4o)      │    └──────────────────────────────────┘   │
│  └─────────────────┘                                            │
└─────────────────────────┬───────────────────────────────────────┘
                          │
                          ▼
┌─────────────────────────────────────────────────────────────────┐
│                   MCP Integration Layer                          │
│              Neo4jMcpClientFactory.cs                            │
│                                                                  │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │           MCP Client (stdio transport)                   │   │
│  │               ↓                                          │   │
│  │    uvx mcp-neo4j-cypher@0.5.0                           │   │
│  └─────────────────────────────────────────────────────────┘   │
└─────────────────────────┬───────────────────────────────────────┘
                          │
                          ▼
┌─────────────────────────────────────────────────────────────────┐
│                      Neo4j Database                              │
│                   (Crime Investigation Data)                     │
└─────────────────────────────────────────────────────────────────┘
```

## 📁 Project Structure

```
GraphDatabaseDetective/
├── Program.cs                              # Entry point
├── AIConfig.cs                             # Azure OpenAI configuration
├── GraphDatabaseDetectiveAgent.cs          # Agent factory with personality
├── DetectiveDemo.cs                        # Main orchestrator + UI helpers
├── Configuration/
│   └── Neo4jConfiguration.cs               # Neo4j config from environment
└── MCP/
    └── Neo4jMcpClientFactory.cs            # MCP client factory
```

## 🔧 Prerequisites

### Required Software

1. **.NET 9 SDK** or later
2. **Python/uvx** - For MCP server execution
   - Install via [Astral UV](https://github.com/astral-sh/uv)
3. **Neo4j Database** - Cloud (Neo4j Aura) or local instance

### Required Accounts

1. **Azure OpenAI** with deployed `gpt-4o` model
2. **Neo4j** database with crime investigation data

## ⚙️ Configuration

### Environment Variables

Create or set these environment variables:

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

### PowerShell Setup

```powershell
# Set environment variables for the session
$env:NEO4J_URI = "neo4j+s://xxxxx.databases.neo4j.io"
$env:NEO4J_USERNAME = "neo4j"
$env:NEO4J_PASSWORD = "your_password"
$env:NEO4J_DATABASE = "neo4j"

$env:AZURE_OPENAI_ENDPOINT = "https://your-endpoint.openai.azure.com/"
$env:AZURE_OPENAI_API_KEY = "your_api_key"
```

## 🚀 Running the Demo

### Quick Start

```bash
cd GraphDatabaseDetective
dotnet run
```

Or press **F5** in Visual Studio/VS Code.

### What Happens on Startup

1. ✅ Initializes console for UTF-8 support (emojis)
2. 🔐 Validates Neo4j environment variables
3. 🔌 Connects to Neo4j via MCP stdio transport
4. 🔍 Discovers available MCP tools from Neo4j server
5. 🎩 Creates the detective agent with personality
6. 💬 Starts interactive conversation loop

### Exiting

Type `q` or `quit` to end the investigation.

## 💬 Example Interactions

### Crime Investigation

```
You: Show me all unsolved murders in the database

Holmsworth Marot 🕵️: Ah, the cold cases! Let me consult the records...
[Executes Cypher query via MCP]
I've found 3 unsolved murders in our database...
```

### Relationship Analysis

```
You: Who are the known associates of suspect John Smith?

Holmsworth Marot 🕵️: An excellent line of inquiry! The web of connections 
often reveals more than individual facts. Let me trace the relationships...
[Explores graph relationships]
```

### Pattern Recognition

```
You: Find all crimes that occurred near the waterfront

Holmsworth Marot 🕵️: Location, location, location! The geography of crime 
can reveal much about the criminal mind...
```

## 🔑 Key Design Patterns

### 1. MCP Integration

The demo uses MCP (Model Context Protocol) to dynamically discover and use Neo4j tools:

```csharp
// Discover tools from MCP server
var mcpTools = await mcpClient.ListToolsAsync();

// Pass tools to agent
var detective = GraphDatabaseDetectiveAgent.Create([.. mcpTools.Cast<AITool>()]);
```

### 2. Factory Pattern

Complex objects are created through factories:

- `Neo4jMcpClientFactory` - Creates MCP client with stdio transport
- `GraphDatabaseDetectiveAgent.Create()` - Creates configured agent

### 3. Fail-Fast Configuration

Configuration is validated early:

```csharp
var neo4jConfig = Neo4jConfiguration.LoadFromEnvironment();
if (neo4jConfig == null)
{
    ShowNeo4jConfigurationError();
    return;
}
```

## 🎯 Use Cases

| Use Case | Description | Example Query |
|----------|-------------|---------------|
| **Crime Investigation** | Explore specific crimes | "Tell me about case #2024-001" |
| **Suspect Analysis** | Analyze suspect relationships | "Who are John's known associates?" |
| **Pattern Detection** | Find crime patterns | "Show me burglary trends this month" |
| **Network Analysis** | Trace criminal networks | "Map the connections in this gang" |
| **Geographic Analysis** | Location-based queries | "Crimes within 1km of the bank" |

## 🐛 Troubleshooting

### Common Issues

| Issue | Cause | Solution |
|-------|-------|----------|
| "Neo4j environment variables not configured" | Missing env vars | Set all required environment variables |
| MCP connection fails | uvx not installed | Install Astral UV: `pip install uv` |
| Empty results | No crime data | Seed your Neo4j database with crime data |
| Azure OpenAI errors | Invalid credentials | Verify endpoint and API key |

### Debug Mode

Add logging to see MCP tool calls:

```csharp
// In DetectiveDemo.cs, add after tool discovery
foreach (var tool in mcpTools)
{
    Console.WriteLine($"  • {tool.Name}: {tool.Description}");
}
```

## 📚 Further Reading

- [Microsoft Agent Framework Documentation](https://github.com/microsoft/agents)
- [Model Context Protocol Specification](https://modelcontextprotocol.io/)
- [Neo4j Cypher Query Language](https://neo4j.com/docs/cypher-manual/)
- [Neo4j MCP Server](https://github.com/neo4j-contrib/mcp-neo4j)

## 🤝 Credits

Created in collaboration with **Zaid Zaim** ([@zaidzaim](https://github.com/zaidzaim)) from Neo4j, who provided invaluable expertise in:

- Neo4j MCP integration
- Environment configuration
- Knowledge graph design
- Crime database setup

Special thanks for the countless brainstorming sessions that brought Holmsworth Marot to life! 🎩🔍
