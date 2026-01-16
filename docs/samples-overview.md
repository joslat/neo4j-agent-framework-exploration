# Samples Overview

This repository contains two main sample projects that demonstrate different aspects of integrating Neo4j graph databases with AI agents using the Microsoft Agent Framework and MCP (Model Context Protocol).

## 📊 Sample Comparison

| Aspect | GraphDatabaseDetective | FinNewsImpactGraph |
|--------|----------------------|-------------------|
| **Domain** | Crime Investigation | Financial Risk Analysis |
| **Focus** | Interactive AI Agent with Natural Language | Graph Analytics & Risk Propagation |
| **Neo4j Interaction** | MCP (Model Context Protocol) | Direct Neo4j.Driver + MCP Chat |
| **Primary Use Case** | Conversational Investigation | Risk Scoring & Visualization |
| **Complexity** | Beginner-Intermediate | Intermediate-Advanced |
| **Best For** | Learning MCP + AI Agents | Learning Graph Analytics |

---

## 🕵️ GraphDatabaseDetective

### Overview

An interactive AI-powered detective agent that investigates crimes using a Neo4j graph database. The agent, named "Holmsworth Marot," embodies traits from classic Golden Age detectives and uses natural language to explore crime networks, relationships, and patterns.

### Key Features

- 🎭 **AI Detective Personality** - Engaging Holmsworth Marot character inspired by Poirot, Holmes, and Marple
- 🔗 **MCP Integration** - Dynamic tool discovery and execution via Model Context Protocol
- 💬 **Natural Language Interface** - Ask questions in plain English
- 🕸️ **Graph Relationship Analysis** - Uncover hidden connections between suspects, victims, and crimes
- 🔍 **Pattern Recognition** - Analyze crime patterns across locations, time, and types

### Technology Stack

- .NET 9 / C# 13.0
- Microsoft.Agents.AI Framework
- Azure OpenAI (gpt-4o-chat)
- Neo4j via MCP stdio transport
- ModelContextProtocol client library

### Example Queries

```
"Show me all unsolved murders in the database"
"Who are the known associates of suspect John Smith?"
"Find all crimes that occurred near the waterfront"
"What connections exist between these two victims?"
"Show me patterns in theft crimes over the last year"
```

### 📖 [Full Guide →](graphdatabasedetective-guide.md)

---

## 📈 FinNewsImpactGraph

### Overview

A financial contagion prototype demonstrating how negative news events propagate risk through corporate networks. Uses Neo4j graph analytics to model ownership, partnership, and supply-chain relationships, calculating impact scores as shocks cascade through the network.

### Key Features

- 📊 **Financial Graph Model** - Companies, markets, countries, sectors, and news events
- ⚡ **Risk Propagation Algorithm** - Weighted path calculations with hop-distance decay
- 📰 **News Event Impact Analysis** - Sentiment-based risk scoring
- 🎨 **Neo4j Browser Visualization** - Interactive graph exploration
- 🐳 **Docker-based Environment** - Easy local development setup
- 🤖 **Two Demo Apps**:
  - **Analytics Runner** - Direct Cypher queries for risk calculation
  - **AI Chat Interface** - Natural language exploration via MCP

### Technology Stack

- .NET 10 / C# 13.0
- Neo4j Community Edition (Docker)
- Neo4j.Driver for Bolt protocol
- Azure OpenAI integration (chat app)
- MCP for natural language queries
- APOC procedures

### Use Cases

- Supply chain risk assessment
- Financial contagion modeling
- Corporate relationship network analysis
- What-if scenario testing for market shocks

### Graph Schema

```
Company ─[OWNS]─> Company
Company ─[SUPPLIES_TO]─> Company
Company ─[PARTNERS_WITH]─> Company
Company ─[LISTED_ON]─> Market
Company ─[HQ_IN]─> Country
NewsEvent ─[AFFECTS]─> Company
```

### 📖 [Full Guide →](finnewsimpactgraph-guide.md)

---

## 🔧 Common Prerequisites

Both samples require:

1. **Azure OpenAI Account** with a deployed gpt-4o model
2. **Neo4j Database** (cloud or local)
3. **Python/uvx** for MCP server (`mcp-neo4j-cypher`)

### Environment Variables

```bash
# Neo4j Database
NEO4J_URI=neo4j+s://xxxxx.databases.neo4j.io  # or neo4j://localhost:7687
NEO4J_USERNAME=neo4j
NEO4J_PASSWORD=your_password
NEO4J_DATABASE=neo4j                           # Optional

# Azure OpenAI
AZURE_OPENAI_ENDPOINT=https://your-endpoint.openai.azure.com/
AZURE_OPENAI_API_KEY=your_api_key
```

---

## 🎯 Which Sample Should I Start With?

### Start with GraphDatabaseDetective if you want to:
- Learn MCP integration patterns
- Build conversational AI agents
- See a simple, engaging demo
- Explore graph relationships through natural language

### Start with FinNewsImpactGraph if you want to:
- Learn graph analytics algorithms
- Understand risk propagation modeling
- Work with Docker and local Neo4j
- See production-style graph schemas

---

## 🚀 Future Samples (Planned)

1. **GraphRAG Implementation** - Using graph structures for enhanced RAG scenarios
2. **Agentic Memory Systems** - Persistent, graph-based memory for AI agents
3. **Fraud Detection Pipeline** - Real-time pattern detection with agentic workflows
4. **Multi-Agent Collaboration** - Agents working together using shared graph knowledge
5. **Temporal Graph Analysis** - Time-series investigation patterns
