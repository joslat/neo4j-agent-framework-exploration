# Neo4j Agent Framework Exploration

Repository for exploring Graph Databases with Neo4j using the Microsoft Agent Framework, with a focus on MCP (Model Context Protocol) integration and advanced agentic AI scenarios.

## Project Goals

This repository explores innovative use cases combining Neo4j graph databases with AI agents, including:

- **Knowledge Graph** - Using Neo4j to represent and query complex domain relationships with structured knowledge graphs that enable sophisticated analytical reasoning
  - **Graph Database Detective** (current demo) - Interactive crime investigation using graph relationships to analyze criminal networks, patterns, and connections
  - **FinNewsImpactGraph** (current demo) - Financial contagion modeling demonstrating risk propagation through corporate networks, performing graph analytics like impact scoring and path-based reasoning across ownership, partnership, and supply-chain relationships
- **GraphRAG** - Retrieval Augmented Generation powered by graph structures
- **Agentic Memory** - Long-term memory systems for AI agents using graph databases
- **Fraud Detection** - Real-time fraud pattern detection using RAG and agentic AI
- **Advanced MCP Integration** - Exploring Model Context Protocol patterns with Neo4j

## Current Projects

### 1. GraphDatabaseDetective

An interactive detective agent demo that uses Neo4j graph database to investigate crimes, analyze relationships, and uncover patterns.

**Key Features:**
- AI agent with Golden Age detective personality ("Holmsworth Marot")
- Real-time Neo4j graph database queries via MCP
- Natural language investigation interface
- Pattern analysis across crime networks
- Deductive reasoning with graph relationships

**Technology Stack:**
- .NET 9 / C# 13.0
- Microsoft.Agents.AI Framework
- Azure OpenAI (gpt-4o-chat)
- Neo4j (via MCP stdio transport)
- ModelContextProtocol client library

📂 [View Project](GraphDatabaseDetective/)

### 2. FinNewsImpactGraph

A financial contagion prototype demonstrating risk propagation through corporate networks using Neo4j graph analytics. Shows how negative news events can cascade through ownership, partnership, and supply-chain relationships.

**Key Features:**
- Financial graph model with companies, markets, countries, and sectors
- Risk propagation algorithm using weighted graph paths
- News event impact analysis with sentiment scoring
- Interactive Neo4j Browser visualization of propagation neighborhoods
- Two demo apps: analytics runner + AI chat interface with MCP integration
- Docker-based local development environment

**Technology Stack:**
- .NET 10 / C# 13.0
- Neo4j Community Edition (Docker)
- Neo4j.Driver for Bolt protocol
- Azure OpenAI integration (chat app)
- MCP for natural language graph queries
- APOC procedures for advanced analytics

**Use Cases:**
- Supply chain risk assessment
- Financial contagion modeling
- Corporate relationship network analysis
- What-if scenario testing for market shocks

📂 [View Project](FinNewsImpactGraph/) | 📖 [Architecture](FinNewsImpactGraph/ARCHITECTURE.md) | 📊 [Schema](FinNewsImpactGraph/GraphDbSchema.md)

## Getting Started

### Prerequisites

- .NET 9 SDK
- Azure OpenAI account with deployed gpt-4o model
- Neo4j database instance (cloud or local)
- Python/uvx for MCP server (`mcp-neo4j-cypher`)

### Environment Variables

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

### Running the Detective Demo

1. Set up environment variables (see above)
2. Navigate to the project: `cd GraphDatabaseDetective`
3. Run: `dotnet run` or press F5 in Visual Studio
4. Start investigating crimes using natural language!
5. Type `q` or `quit` to exit

**Example Queries:**
- "Show me all unsolved murders in the database"
- "Who are the known associates of suspect John Smith?"
- "Find all crimes that occurred near the waterfront"
- "What connections exist between these two victims?"

## Documentation

- [ARCHITECTURE.md](ARCHITECTURE.md) - Detailed project structure and design decisions
- See individual project folders for specific documentation

## Collaboration

This demo was created in collaboration with **Zaid Zaim** ([@zaidzaim](https://github.com/zaidzaim)), who provided invaluable support with Neo4j MCP integration, environment configuration, and countless ideas that brought this detective to life!

## Future Explorations

We plan to expand this repository with additional demos showcasing:

1. **GraphRAG Implementation** - Using graph structures for enhanced RAG scenarios
2. **Agentic Memory Systems** - Persistent, graph-based memory for AI agents
3. **Fraud Detection Pipeline** - Real-time pattern detection with agentic workflows
4. **Multi-Agent Collaboration** - Agents working together using shared graph knowledge
5. **Temporal Graph Analysis** - Time-series investigation patterns

## License

See [LICENSE](LICENSE) file for details.

## Acknowledgments

Special thanks to Zaid Zaim for his expertise in graph databases and MCP integration, and for the collaborative brainstorming sessions that made this project possible!
