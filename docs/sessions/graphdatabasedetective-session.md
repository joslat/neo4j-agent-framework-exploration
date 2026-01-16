# GraphDatabaseDetective - 30-Minute Session Plan

## 📋 Session Overview

| Field | Details |
|-------|---------|
| **Title** | Graph Database Detective: Building AI Agents with Neo4j and MCP |
| **Duration** | 30 minutes |
| **Presenters** | **Jose Luis Latorre** (AI/Agent Development) & **Zaid Zaim** (Neo4j/Knowledge Graphs) |
| **Level** | Intermediate |
| **Format** | Live Demo + Technical Deep Dive |

## 🎯 Session Objectives

By the end of this session, attendees will:

1. Understand how to integrate Neo4j with AI agents using MCP (Model Context Protocol)
2. See the power of graph databases for relationship-based reasoning
3. Learn patterns for building conversational AI agents that query knowledge graphs
4. Appreciate the synergy between AI reasoning and graph traversal

## 👥 Presenter Roles

### Jose Luis Latorre
- AI/Agent architecture overview
- Microsoft Agent Framework integration
- MCP client implementation
- Live demo driving

### Zaid Zaim (Neo4j Expert)
- Knowledge graph concepts
- Neo4j graph modeling
- Cypher query optimization
- Graph relationship patterns

---

## 📊 High-Level Session Outline

| # | Section | Duration | Focus | Primary Presenter |
|---|---------|----------|-------|-------------------|
| 1 | **Opening & Introduction** | 2 min | Set the stage, introduce presenters & collaboration story | Zaid → Jose Luis |
| 2 | **What is a Graph Database?** | 2 min | Graph DB fundamentals, nodes, relationships, properties | Zaid |
| 3 | **Sponsor Acknowledgment** | 0.5 min | Thank Neo4j for sponsoring | Jose Luis |
| 4 | **Graph DBs in Agentic AI & GraphRAG** | 3.5 min | Knowledge Graphs, GraphRAG vs Traditional RAG, Agentic AI | Zaid → Jose Luis |
| 5 | **The Problem & Solution** | 4 min | Why MCP? Architecture overview | Jose Luis |
| 6 | **Meet Holmsworth Marot** | 2.5 min | Detective persona, crime database schema | Jose Luis → Zaid |
| 7 | **Live Demo: Investigation** | 10 min | Interactive queries, MCP in action, graph traversals | Both (alternating) |
| 8 | **Technical Deep Dive** | 3.5 min | Code walkthrough, MCP integration, agent design | Jose Luis → Zaid |
| 9 | **Takeaways & Q&A** | 2.5 min | Key learnings, resources, audience questions | Both |

---

## 📋 Detailed Session Breakdown

### Section 1: Opening & Introduction (0:00 - 2:00)

| Time | Activity | Who | How | Visual/Tool |
|------|----------|-----|-----|-------------|
| 0:00 | Welcome & hook | Zaid | "Welcome! Today we're going to solve crimes using AI and graph databases!" | Title slide |
| 0:20 | Introduce himself | Zaid | Brief intro: Neo4j, Knowledge Graphs expertise | Photo/slide |
| 0:40 | Introduce Jose Luis | Zaid | "My partner in crime-solving today..." + collaboration story | Photo/slide of both |
| 1:00 | Jose takes over | Jose Luis | Thank Zaid, express excitement about the collaboration | Verbal |
| 1:20 | Session roadmap | Jose Luis | Quick overview of what attendees will learn | Agenda slide |
| 1:40 | Set expectations | Jose Luis | "By the end, you'll understand MCP + Neo4j integration" | Objectives slide |

**Zaid's Opening Script:**
> "Welcome everyone! I'm Zaid Zaim from Neo4j, and today we're going to do something fun - we're going to solve crimes using AI and graph databases! But I'm not doing this alone. Let me introduce my partner in crime-solving, Jose Luis Latorre, who's an expert in AI agents and the Microsoft Agent Framework. Together, we built this demo and we're excited to show you what's possible when you combine graph databases with agentic AI."

---

### Section 2: What is a Graph Database? (2:00 - 4:00)

| Time | Activity | Who | How | Visual/Tool |
|------|----------|-----|-----|-------------|
| 2:00 | Graph DB fundamentals | Zaid | "A graph is nodes connected by relationships" | Simple graph diagram |
| 2:30 | Nodes, relationships, properties | Zaid | Visual example: Person -[KNOWS]-> Person | Animated diagram |
| 3:00 | Why graphs matter | Zaid | "Relationships are first-class citizens, not JOINs" | Comparison visual |
| 3:30 | Neo4j introduction | Zaid | World's leading graph database, Cypher query language | Neo4j logo + Cypher example |

**Zaid's Key Points:**
- "In a graph, relationships are as important as the data itself"
- "Think of it like a whiteboard - you naturally draw circles and arrows"
- "Cypher reads like English: MATCH (person)-[:KNOWS]->(friend)"

---

### Section 3: Sponsor Acknowledgment (4:00 - 4:30)

| Time | Activity | Who | How | Visual/Tool |
|------|----------|-----|-----|-------------|
| 4:00 | Thank Neo4j | Jose Luis | "Special thanks to Neo4j for sponsoring this session" | Neo4j logo slide |
| 4:15 | Acknowledge partnership | Jose Luis | Brief mention of Neo4j's support for the community | Sponsor slide |

**Jose Luis's Script:**
> "Before we dive deeper, I want to take a moment to thank Neo4j for sponsoring this session. Their support makes events like this possible, and we're excited to showcase what you can build with their technology."

---

### Section 4: Graph DBs in Agentic AI, Knowledge Graphs & GraphRAG (4:30 - 8:00)

| Time | Activity | Who | How | Visual/Tool |
|------|----------|-----|-----|-------------|
| 4:30 | The AI context problem | Zaid | "LLMs need structured knowledge to reason accurately" | Problem statement slide |
| 5:00 | What are Knowledge Graphs? | Zaid | "Structured representation of real-world entities & relationships" | KG diagram |
| 5:30 | Traditional RAG explained | Zaid | Vector embeddings, similarity search, chunk retrieval | RAG diagram |
| 5:50 | Traditional RAG limitations | Zaid | "Chunks lack context, no relationships, semantic gaps" | Bullet points |
| 6:10 | **GraphRAG introduction** | Zaid | "Combine graph structure with retrieval for richer context" | GraphRAG diagram |
| 6:30 | GraphRAG vs Traditional RAG | Zaid | Side-by-side comparison: relationships, multi-hop, context | Comparison table |
| 6:50 | Graph DBs in Agentic AI | Jose Luis | "Agents need to query, reason, and act on connected data" | Agent architecture |
| 7:20 | Why graphs for agents? | Jose Luis | Relationship traversal, context retrieval, memory | Bullet points |
| 7:40 | Real-world applications | Both | Fraud detection, recommendations, investigations | Examples slide |

**Key Concepts to Cover:**

#### Knowledge Graphs
- **Definition**: Entities + Relationships + Properties = Structured Knowledge
- **Why AI needs graphs**: Context grounding, relationship reasoning, reducing hallucinations

#### Traditional RAG vs GraphRAG

| Aspect | Traditional RAG (Vector Store) | GraphRAG (Knowledge Graph) |
|--------|-------------------------------|---------------------------|
| **Data Structure** | Flat chunks with embeddings | Nodes + Relationships |
| **Retrieval** | Similarity search (k-nearest) | Graph traversal + patterns |
| **Context** | Limited to chunk boundaries | Rich relationship context |
| **Multi-hop** | ❌ Cannot follow connections | ✅ Natural path traversal |
| **Reasoning** | Surface-level similarity | Deep structural reasoning |
| **Updates** | Re-embed entire documents | Add/update nodes & edges |
| **Explainability** | "Similar to X" | "Connected via Y relationship" |

**Zaid's GraphRAG Talking Points:**
- "Traditional RAG treats documents as isolated chunks - you lose the connections"
- "GraphRAG preserves relationships: 'John works at Acme' becomes a queryable edge"
- "When you ask 'Who does John work with?', GraphRAG traverses relationships, not just finds similar text"
- "Think of it as giving your AI a map instead of a pile of sticky notes"

#### Agentic AI Use Cases
- Memory systems (agent remembers past interactions via graph)
- Tool augmentation (agent queries graph for context)
- Multi-hop reasoning (follow relationship chains)

**Handoff:** Zaid covers KG + GraphRAG fundamentals → Jose Luis connects to Agentic AI patterns

---

### Section 5: The Problem & Solution (7:30 - 11:30)

| Time | Activity | Who | How | Visual/Tool |
|------|----------|-----|-----|-------------|
| 7:30 | Our specific challenge | Jose Luis | "How do we let an AI agent query a graph database?" | Problem slide |
| 8:00 | Traditional approach problems | Jose Luis | Hardcoded queries, no flexibility, maintenance burden | Bullet points |
| 8:30 | What is MCP? | Jose Luis | Model Context Protocol bridges AI ↔ Tools | MCP architecture diagram |
| 9:00 | MCP benefits | Jose Luis | Dynamic tool discovery, standardized interface | Bullet points |
| 9:30 | Our architecture | Jose Luis | User → Agent → MCP → Neo4j flow | Architecture diagram |
| 10:00 | Technology stack | Jose Luis | .NET, Azure OpenAI, mcp-neo4j-cypher | Tech logos slide |
| 10:30 | The mcp-neo4j-cypher server | Jose Luis | Provides read_neo4j_cypher, get_neo4j_schema tools | Tool list |
| 11:00 | How it all connects | Jose Luis | Agent discovers tools at runtime, generates Cypher | Flow diagram |

**Jose Luis's Talking Points:**
- "MCP is like a universal adapter - the agent learns what tools are available"
- "Instead of hardcoding queries, the agent generates them based on user intent"
- "The Neo4j MCP server exposes graph capabilities as discoverable tools"

---

### Section 6: Meet Holmsworth Marot (11:30 - 14:00)

| Time | Activity | Who | How | Visual/Tool |
|------|----------|-----|-----|-------------|
| 11:30 | Introduce detective persona | Jose Luis | Read the system prompt, explain personality design | Slide with prompt excerpt |
| 12:00 | Why personality matters | Jose Luis | UX engagement, memorable demos | Bullet points |
| 12:30 | Crime database schema | Zaid | Explain nodes: Person, Crime, Location, Evidence | Schema diagram |
| 13:00 | Relationships in the graph | Zaid | KNOWS, WITNESSED, COMMITTED, LOCATED_AT | Relationship types slide |
| 13:30 | Quick Neo4j Browser preview | Zaid | Show sample data visually | **Live: Neo4j Browser** |

**Handoff:** Jose Luis sets up persona → Zaid shows the data model

---

### Section 7: Live Demo - Investigation (14:00 - 24:00)

| Time | Activity | Who | How | Visual/Tool |
|------|----------|-----|-----|-------------|
| 14:00 | Start the application | Jose Luis | Run `dotnet run`, show startup output | **Live: Terminal** |
| 14:30 | Explain MCP connection | Jose Luis | Point out "Connected to MCP", "3 tools discovered" | Terminal output |
| 15:00 | **Query 1:** "Show unsolved murders" | Jose Luis | Type query, wait for response | **Live: Detective app** |
| 15:30 | Explain MCP tool call | Jose Luis | "The agent chose to call read_neo4j_cypher" | Terminal/response |
| 16:00 | Show generated Cypher | Zaid | Highlight the MATCH pattern, filters | Cypher in response |
| 16:30 | Visualize in Neo4j Browser | Zaid | Run same query in Browser, show graph | **Live: Neo4j Browser** |
| 17:30 | **Query 2:** "Who knows [suspect]?" | Jose Luis | Relationship traversal query | **Live: Detective app** |
| 18:00 | Explain graph traversal | Zaid | "This is WHERE graphs shine - hop traversal!" | Whiteboard/diagram |
| 18:30 | Show 2-hop visualization | Zaid | Expand the suspect's network visually | **Live: Neo4j Browser** |
| 19:30 | **Query 3:** "Crimes near waterfront" | Jose Luis | Location-based pattern detection | **Live: Detective app** |
| 20:00 | AI reasoning explanation | Jose Luis | Show how AI combines multiple data points | Response analysis |
| 20:30 | Location graph visualization | Zaid | Cluster crimes by location in Browser | **Live: Neo4j Browser** |
| 21:30 | **Query 4:** "Connections between victims" | Jose Luis | Complex multi-hop relationship query | **Live: Detective app** |
| 22:00 | Path finding explanation | Zaid | Shortest path, relationship chains | Graph visualization |
| 23:00 | **Query 5:** (Optional) Audience suggestion | Both | Take a question, run it live | **Live: Detective app** |
| 23:30 | Demo wrap-up | Jose Luis | Summarize what we demonstrated | Verbal |

**Demo Flow Pattern:**
1. Jose Luis types query → Shows response
2. Jose Luis explains MCP/Agent behavior
3. Zaid explains the Cypher/Graph concepts
4. Zaid visualizes in Neo4j Browser
5. Repeat with next query

---

### Section 8: Technical Deep Dive (24:00 - 27:30)

| Time | Activity | Who | How | Visual/Tool |
|------|----------|-----|-----|-------------|
| 24:00 | MCP client factory code | Jose Luis | Show `Neo4jMcpClientFactory.CreateAsync()` | **Code slide/VS Code** |
| 24:30 | Tool discovery code | Jose Luis | Show `ListToolsAsync()` and tool attachment | **Code slide/VS Code** |
| 25:00 | Agent creation code | Jose Luis | Show `GraphDatabaseDetectiveAgent.Create()` | **Code slide/VS Code** |
| 25:30 | Neo4j MCP server capabilities | Zaid | `mcp-neo4j-cypher` tools: read, schema | Bullet points |
| 26:00 | Security considerations | Zaid | Read-only by default, credential handling | Bullet points |
| 26:30 | Agent personality prompt | Jose Luis | Show system prompt design | Prompt text slide |
| 27:00 | Extensibility options | Both | Add more tools, custom Cypher, other DBs | Bullet points |

**Code Highlights to Show:**

```csharp
// 1. MCP Client Creation
await using var mcpClient = await Neo4jMcpClientFactory.CreateAsync(neo4jConfig);

// 2. Tool Discovery
var mcpTools = await mcpClient.ListToolsAsync();
Console.WriteLine($"Discovered {mcpTools.Count} tools!");

// 3. Agent Creation with Tools
var detective = GraphDatabaseDetectiveAgent.Create([.. mcpTools.Cast<AITool>()]);
```

---

### Section 9: Takeaways & Q&A (27:30 - 30:00)

| Time | Activity | Who | How | Visual/Tool |
|------|----------|-----|-----|-------------|
| 27:30 | Key takeaway 1: Knowledge Graphs | Zaid | "Knowledge Graphs ground AI in structured facts" | Takeaways slide |
| 27:45 | Key takeaway 2: MCP | Jose Luis | "MCP enables dynamic tool discovery" | Takeaways slide |
| 28:00 | Key takeaway 3: Graphs + AI | Both | "Graph traversal + AI reasoning = Powerful insights" | Takeaways slide |
| 28:15 | When to use this approach | Zaid | Investigation, fraud, recommendations, memory | Use cases slide |
| 28:30 | Resources & links | Both | GitHub repo, docs, Neo4j MCP server | Resources slide |
| 29:00 | Q&A | Both | Take 2-3 audience questions | Open floor |
| 29:45 | Closing & thank you | Jose Luis | Thank Zaid, thank Neo4j, thank audience | Closing slide |

**Key Takeaways Slide:**

| # | Takeaway | Owner |
|---|----------|-------|
| 1 | ✅ **Knowledge Graphs ground AI** - Structured facts reduce hallucinations and enable reasoning | Zaid |
| 2 | ✅ **MCP enables dynamic tool discovery** - AI agents learn available capabilities at runtime | Jose Luis |
| 3 | ✅ **Graph DBs excel at relationships** - Perfect for investigation, fraud, network analysis | Zaid |
| 4 | ✅ **Personality enhances UX** - Detective character makes the demo memorable and engaging | Jose Luis |
| 5 | ✅ **AI + Graph = Powerful insights** - Combine reasoning with relationship traversal | Both |

---

## 📅 Quick Reference Timeline

| Start | End | Section | Lead |
|-------|-----|---------|------|
| 0:00 | 2:00 | Opening & Introduction | Zaid → Jose Luis |
| 2:00 | 4:00 | What is a Graph Database? | Zaid |
| 4:00 | 4:30 | Sponsor Acknowledgment (Neo4j) | Jose Luis |
| 4:30 | 8:00 | Graph DBs in Agentic AI, KGs & GraphRAG | Zaid → Jose Luis |
| 7:30 | 11:30 | The Problem & Solution | Jose Luis |
| 11:30 | 14:00 | Meet Holmsworth Marot | Jose Luis → Zaid |
| 14:00 | 24:00 | Live Demo: Investigation | Both (alternating) |
| 24:00 | 27:30 | Technical Deep Dive | Jose Luis → Zaid |
| 27:30 | 30:00 | Takeaways & Q&A | Both |

---

## 🎨 Presentation Materials

### Required Slides

1. **Title Slide** - Session title, presenters, logos
2. **Architecture Diagram** - User → Agent → MCP → Neo4j
3. **Graph Schema** - Crime database visualization
4. **Code Highlights** - Key integration points
5. **Takeaways** - 5 bullet points
6. **Resources** - Links to repo, docs, and tools

### Demo Prerequisites

- [ ] Neo4j database running with crime data
- [ ] Environment variables configured
- [ ] VS Code / Visual Studio ready
- [ ] Neo4j Browser open in separate tab
- [ ] Terminal ready to run `dotnet run`
- [ ] Backup screenshots prepared

### Backup Plan

If live demo fails:
1. Show pre-recorded video (2-3 minutes)
2. Walk through code with screenshots
3. Focus on architecture and concepts

---

## 🔗 Resources to Share

### During Session
- GitHub Repository: `github.com/joslat/neo4j-agent-framework-exploration`
- Neo4j MCP Server: `github.com/neo4j-contrib/mcp-neo4j`

### After Session
- Documentation: `/docs` folder in repo
- Architecture: `GraphDatabaseDetective/ARCHITECTURE.md`
- This session plan: `/docs/sessions/graphdatabasedetective-session.md`

---

## 💡 Speaker Notes

### Jose Luis
- Emphasize the simplicity of MCP integration
- Show excitement about AI + Graph synergy
- Keep energy high during demo
- Defer graph-specific questions to Zaid

### Zaid
- Highlight Neo4j's relationship strengths
- Use visual examples when explaining Cypher
- Mention real-world use cases (fraud, recommendations)
- Keep technical depth appropriate for audience

### Handoff Points
- Jose introduces topic → Zaid explains graph concept
- Jose runs query → Zaid explains the Cypher
- Jose shows code → Zaid shows Neo4j Browser

---

## 🎯 Success Metrics

The session is successful if:
- [ ] Live demo completes without major issues
- [ ] Audience understands MCP integration pattern
- [ ] At least 2-3 audience questions during Q&A
- [ ] Clear collaboration demonstrated between presenters
- [ ] Resources shared and accessible

---

## 📝 Post-Session

1. Share slides and recording (if available)
2. Tweet/post about the session with repo link
3. Answer follow-up questions via GitHub Issues
4. Consider blog post expanding on concepts

---

*Session plan created for Jose Luis Latorre & Zaid Zaim*
*GraphDatabaseDetective Demo - Neo4j + Microsoft Agent Framework + MCP*

---

## 🎭 Storytelling Approach (Alternative Narrative Version)

This section explores how to transform the technical session into a more engaging storytelling experience. Consider this for future iterations or longer session formats.

### The Narrative Arc

**Setting:** A mysterious crime has occurred in the city. The police are stumped. They've called in a legendary detective - but this detective has a secret weapon: an AI partner connected to a vast knowledge graph of criminal records, relationships, and evidence.

### Story Structure

| Act | Story Beat | Technical Concept | Emotional Hook |
|-----|------------|-------------------|----------------|
| **Act 1: The Setup** | A crime is discovered, traditional methods fail | Why we need graphs | Intrigue, mystery |
| **Act 2: The Tools** | Detective reveals AI partner & knowledge graph | MCP + Neo4j architecture | Wonder, possibility |
| **Act 3: The Investigation** | Following clues through the graph | Live demo queries | Excitement, discovery |
| **Act 4: The Revelation** | Connections reveal the truth | Graph traversal power | Satisfaction, "aha!" |
| **Act 5: The Resolution** | Case solved, lessons learned | Takeaways & applications | Inspiration, motivation |

### Opening Hook Options

**Option A: The Crime Scene**
> *[Slide shows a noir-style crime scene]*
> 
> Zaid: "Last night, a priceless artifact was stolen from the city museum. The police found no fingerprints, no witnesses, no leads. They're about to close the case... but then they called us."
>
> Jose: "What the police don't know is that every person in this city - their relationships, their movements, their connections - they're all mapped in a vast knowledge graph. And we have an AI detective who can traverse it."

**Option B: The Detective Introduction**
> *[Slide shows Holmsworth Marot silhouette]*
>
> Zaid: "Meet Holmsworth Marot. He's not your ordinary detective. He doesn't just follow hunches - he follows relationships. Connections. Patterns that span across hundreds of people and places."
>
> Jose: "And his secret? A graph database brain that never forgets a connection, powered by AI that can reason across thousands of data points in seconds."

**Option C: The Challenge**
> *[Slide shows a complex web of connections]*
>
> Zaid: "Here's a puzzle: How do you find a killer when there are 500 suspects, 200 locations, and 10,000 relationships between them?"
>
> Jose: "You don't search. You traverse. Let us show you how."

### Demo as Investigation Scenes

Instead of "Query 1, Query 2...", frame each query as a scene:

| Scene | Query | Narrative |
|-------|-------|----------|
| **Scene 1: The Victims** | "Show unsolved murders" | "Let's see what cases remain open..." |
| **Scene 2: The Network** | "Who knows suspect X?" | "Every criminal has connections. Let's pull the thread..." |
| **Scene 3: The Pattern** | "Crimes near waterfront" | "Wait... there's a pattern emerging. Look at the locations..." |
| **Scene 4: The Connection** | "Path between victims" | "These victims seemed unrelated... but the graph reveals..." |
| **Scene 5: The Revelation** | Final insight | "And there it is. The connection that solves everything." |

### Character Voices

**Holmsworth Marot (The AI Detective)**
- Speaks in eloquent, Victorian-era detective style
- Makes dramatic observations
- "Fascinating... the graph reveals what the eye cannot see"

**The Presenters as Narrators**
- Zaid: The "graph whisperer" who explains the data structure
- Jose: The "tech translator" who explains how it works

### Visual Storytelling Elements

1. **Noir aesthetic** - Dark slides with dramatic lighting
2. **Evidence board** - Show connections appearing like red string on a corkboard
3. **Reveal moments** - Animations that show paths lighting up in the graph
4. **Character cards** - Show suspects/victims as character profiles

### Emotional Beats to Hit

| Moment | Emotion | How to Create It |
|--------|---------|------------------|
| Opening | Curiosity | Present an unsolved mystery |
| First query | Wonder | Show the AI "thinking" |
| Graph visualization | Awe | Reveal the beautiful complexity |
| Pattern discovery | Excitement | "Did you see that?!" moment |
| Final connection | Satisfaction | The "aha!" payoff |
| Takeaways | Inspiration | "You can build this too" |

### Dialogue Examples

**For the Network Query:**
> Zaid: "In traditional investigations, you'd interview dozens of people one by one. Hours of work."
>
> Jose: "Let's ask our detective..." *types query*
>
> Holmsworth: *responds with connections*
>
> Zaid: "In 3 seconds, the graph traversed 47 relationships and found 12 relevant connections. This is the power of graph databases."

**For the Pattern Discovery:**
> Jose: "Now here's where it gets interesting. Holmsworth, what patterns do you see?"
>
> Holmsworth: *responds with analysis*
>
> Zaid: *pulls up Neo4j Browser* "Look at this cluster. The AI didn't just find text matches - it followed relationship chains and discovered a spatial pattern that would take humans days to notice."

### Closing the Story

> Jose: "The case is solved. But the real story here isn't about crime - it's about what happens when you give AI the power to understand relationships."
>
> Zaid: "Every organization has data with hidden connections. Customer networks. Fraud patterns. Supply chains. The question is: are you just storing data, or are you understanding relationships?"
>
> Jose: "Today you met Holmsworth Marot. Tomorrow, you could build your own AI detective. The code is open source, the tools are available, and the possibilities are endless."
>
> Zaid: "The game is afoot. Thank you."

### When to Use Storytelling Approach

✅ **Good for:**
- Keynotes and general audiences
- Marketing/demo events
- Longer sessions (45+ minutes)
- Recorded content / videos
- Audiences new to the concepts

⚠️ **Consider carefully for:**
- Highly technical audiences (may want more code)
- Short time slots (story takes time)
- Workshop formats (need hands-on focus)

### Hybrid Approach

For the current 30-minute session, consider:
- Use storytelling **hook** in opening (1 minute)
- Frame demo queries as **investigation scenes** (keeps energy)
- Add **reveal moments** with dramatic pauses
- Close with **narrative wrap-up** (30 seconds)
- Keep technical depth but with story context
