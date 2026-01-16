# FinNewsImpactGraph - Complete Guide

## Overview

FinNewsImpactGraph is a financial contagion prototype that demonstrates how negative news events propagate risk through corporate networks using Neo4j graph analytics. The project models ownership, partnership, and supply-chain relationships to calculate impact scores as shocks cascade through the financial network.

## 🎯 Key Concepts

### Financial Contagion

Financial contagion refers to how a shock affecting one entity can spread to others through interconnected relationships:

```
News Event (Negative Sentiment)
        │
        ▼
    Company A ────[AFFECTS]────
        │                      
        │ [SUPPLIES_TO]        
        ▼                      
    Company B                  
        │                      
        │ [PARTNERS_WITH]      
        ▼                      
    Company C                  
```

### Risk Propagation Algorithm

The demo calculates risk scores using:

1. **Direct Impact** - From the `AFFECTS` relationship with the news event
2. **Indirect Impact** - Flows through graph paths with decay
3. **Edge Weights** - `pct` (ownership), `criticality` (supply chain), `strength` (partnership)
4. **Hop Distance** - Risk decays with each hop

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                    Two Demo Applications                         │
├────────────────────────────┬────────────────────────────────────┤
│   FinNewsImpactGraphDemo   │   FinNewsImpactGraphAgentChat      │
│   (Analytics Runner)       │   (AI Chat Interface)              │
│                            │                                    │
│   • Direct Cypher queries  │   • Natural language queries       │
│   • Risk calculation       │   • MCP integration                │
│   • Stats reporting        │   • Azure OpenAI                   │
└────────────────────────────┴────────────────────────────────────┘
                          │
                          ▼
┌─────────────────────────────────────────────────────────────────┐
│                      Neo4j Database                              │
│                  (Docker - Community Edition)                    │
│                                                                  │
│   ┌─────────────────────────────────────────────────────────┐   │
│   │  Companies, Markets, Countries, Sectors, NewsEvents     │   │
│   │                                                         │   │
│   │  Relationships:                                         │   │
│   │  OWNS, SUPPLIES_TO, PARTNERS_WITH, LISTED_ON,          │   │
│   │  HQ_IN, OPERATES_IN, IN_SECTOR, AFFECTS                │   │
│   └─────────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────┘
```

## 📁 Project Structure

```
FinNewsImpactGraph/
├── docker-compose.yml              # Neo4j container configuration
├── FinNewsImpactGraph.sln          # Solution file
├── ARCHITECTURE.md                 # Detailed architecture docs
├── GraphDbSchema.md                # Database schema reference
├── README.md                       # Quick start guide
├── neo4j/
│   ├── conf/
│   │   └── neo4j.conf              # Neo4j configuration
│   └── import/
│       └── seed.cypher             # Sample dataset
└── src/
    ├── FinNewsImpactGraphDemo/         # Analytics runner
    │   ├── FinNewsImpactGraphDemo.csproj
    │   └── Program.cs
    └── FinNewsImpactGraphAgentChat/    # AI chat interface
        ├── FinNewsImpactGraphAgentChat.csproj
        ├── Program.cs
        ├── MarketNewsChatDemo.cs
        ├── MarketNewsGraphAgent.cs
        ├── Configuration/
        │   ├── AIConfig.cs
        │   └── Neo4jConfiguration.cs
        └── MCP/
            └── Neo4jMcpClientFactory.cs
```

## 📊 Graph Schema

### Node Labels

| Label | Key Property | Description |
|-------|--------------|-------------|
| `Company` | `ticker` | Public companies (NVDA, TSLA, MSFT, etc.) |
| `Country` | `code` | Countries (US, TW, CN, etc.) |
| `Market` | `code` | Stock exchanges (NASDAQ, NYSE, etc.) |
| `Sector` | `name` | Industry sectors (Technology, AI, etc.) |
| `NewsEvent` | `id` | News articles with sentiment |

### Relationships

| Relationship | Properties | Description |
|--------------|------------|-------------|
| `OWNS` | `pct` | Equity ownership (0-100%) |
| `SUPPLIES_TO` | `criticality` | Supply chain (0-1) |
| `PARTNERS_WITH` | `strength` | Partnership (0-1) |
| `LISTED_ON` | - | Company → Market |
| `HQ_IN` | - | Company → Country |
| `OPERATES_IN` | - | Company → Country |
| `IN_SECTOR` | - | Company → Sector |
| `AFFECTS` | `directImpact` | NewsEvent → Company |

### Schema Diagram

```
Company ─[OWNS]─────────────> Company
Company ─[SUPPLIES_TO]──────> Company
Company ─[PARTNERS_WITH]────> Company
Company ─[LISTED_ON]────────> Market
Company ─[HQ_IN]────────────> Country
Company ─[OPERATES_IN]──────> Country
Company ─[IN_SECTOR]────────> Sector
NewsEvent ─[AFFECTS]────────> Company
NewsEvent ─[ASSOCIATED_WITH]> Market
```

## 🔧 Prerequisites

### Required Software

1. **Docker Desktop** - For running Neo4j
2. **.NET 10 SDK** - For running the demo apps
3. **Python/uvx** - For MCP server (chat app only)

### Port Requirements

- `7474` - Neo4j Browser (HTTP)
- `7687` - Neo4j Bolt (drivers)

## 🚀 Getting Started

### Step 1: Start Neo4j

```powershell
cd FinNewsImpactGraph
docker compose up -d
```

Verify Neo4j is running:
- Browser: http://localhost:7474
- Login: `neo4j` / `neo4j-password-change-me`

### Step 2: Seed Data (Optional)

The app auto-seeds on first run, but you can manually seed:

```cypher
:use neo4j
:source /import/seed.cypher
```

### Step 3: Run the Analytics Demo

```powershell
# Set credentials
$env:NEO4J_URI = "neo4j://localhost:7687"
$env:NEO4J_USER = "neo4j"
$env:NEO4J_PASSWORD = "neo4j-password-change-me"

# Optional: Select news event
$env:NEWS_ID = "N-2025-12-18-001"  # Negative news (default)

# Run
dotnet run --project .\src\FinNewsImpactGraphDemo\FinNewsImpactGraphDemo.csproj
```

### Step 4: Run the Chat App (Optional)

```powershell
# Additional Azure OpenAI config
$env:AZURE_OPENAI_ENDPOINT = "https://your-endpoint.openai.azure.com/"
$env:AZURE_OPENAI_API_KEY = "your-key"

dotnet run --project .\src\FinNewsImpactGraphAgentChat\FinNewsImpactGraphAgentChat.csproj
```

## 📈 Demo Scenarios

### Scenario 1: Negative News Impact

```powershell
$env:NEWS_ID = "N-2025-12-18-001"
dotnet run --project .\src\FinNewsImpactGraphDemo\FinNewsImpactGraphDemo.csproj
```

Shows risk propagation from a negative sentiment news event.

### Scenario 2: Positive News (Baseline)

```powershell
$env:NEWS_ID = "N-2025-12-18-002"
dotnet run --project .\src\FinNewsImpactGraphDemo\FinNewsImpactGraphDemo.csproj
```

Shows near-zero risk scores (model ignores positive sentiment).

### Scenario 3: Supply Chain Shock

```powershell
$env:NEWS_ID = "N-2025-12-18-005"
dotnet run --project .\src\FinNewsImpactGraphDemo\FinNewsImpactGraphDemo.csproj
```

Demonstrates supply-side propagation effects.

## 🔬 What-If Analysis

Modify the graph in Neo4j Browser, then rerun the app:

### Reduce Supply Chain Criticality

```cypher
MATCH (:Company {ticker:'TSMC'})-[r:SUPPLIES_TO]->(:Company {ticker:'NVDA'})
SET r.criticality = 0.2
RETURN r;
```

### Increase Ownership Exposure

```cypher
MATCH (:Company {ticker:'MSFT'})-[r:OWNS]->(:Company {ticker:'GITHUB'})
SET r.pct = 100.0
RETURN r;
```

### Add New Propagation Path

```cypher
MATCH (a:Company {ticker:'NVDA'}), (b:Company {ticker:'OPENAI'})
MERGE (a)-[r:SUPPLIES_TO]->(b)
SET r.criticality = 0.3
RETURN r;
```

## 🎨 Neo4j Browser Visualization

### View the Propagation Neighborhood

```cypher
MATCH (n:NewsEvent {id:'N-2025-12-18-001'})-[a:AFFECTS]->(c0:Company)
MATCH p=(c0)-[:OWNS|PARTNERS_WITH|SUPPLIES_TO*0..2]-(c:Company)
RETURN n, a, p;
```

### View Company Relationships

```cypher
MATCH p=(c0:Company {ticker:'NVDA'})-[:OWNS|PARTNERS_WITH|SUPPLIES_TO*0..2]-(c:Company)
RETURN p;
```

### View Schema

```cypher
CALL db.schema.visualization();
```

## 🔄 Reset & Cleanup

### Stop Neo4j

```powershell
docker compose down
```

### Complete Reset (Wipe Data)

```powershell
docker compose down
Remove-Item -Recurse -Force neo4j/data
docker compose up -d
```

## 📊 Output Files

| File | Description |
|------|-------------|
| `query-audit.jsonl` | All executed Cypher queries (newline-delimited JSON) |

## 🎯 Use Cases

| Use Case | Description |
|----------|-------------|
| **Supply Chain Risk** | Assess how supplier disruptions cascade |
| **Contagion Modeling** | Model financial shock propagation |
| **Network Analysis** | Analyze corporate relationship networks |
| **Scenario Testing** | What-if analysis for market shocks |

## 🐛 Troubleshooting

| Issue | Solution |
|-------|----------|
| Neo4j won't start | Check ports 7474/7687 are free |
| Empty graph | Run `:source /import/seed.cypher` or restart app |
| Password error | Ensure compose password matches env var |
| Zero risk scores | Use a negative sentiment news event |

## 📚 Further Reading

- [ARCHITECTURE.md](../FinNewsImpactGraph/ARCHITECTURE.md) - Detailed technical architecture
- [GraphDbSchema.md](../FinNewsImpactGraph/GraphDbSchema.md) - Complete schema reference
- [Neo4j Cypher Documentation](https://neo4j.com/docs/cypher-manual/)
- [APOC Procedures](https://neo4j.com/labs/apoc/)
