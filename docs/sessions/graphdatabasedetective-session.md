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

## 📅 Session Timeline

### Opening (2 minutes)

| Time | Activity | Presenter |
|------|----------|-----------|
| 0:00 | Welcome & Introduction | Jose Luis |
| 0:30 | Introduce Zaid & Neo4j collaboration | Jose Luis |
| 1:00 | Session roadmap overview | Jose Luis |

**Key Points:**
- "We're going to solve crimes using AI and graph databases!"
- Introduce the detective concept
- Highlight the collaboration story

---

### Part 1: The Problem & Solution (5 minutes)

| Time | Activity | Presenter |
|------|----------|-----------|
| 2:00 | Why graph databases for investigations? | Zaid |
| 3:30 | The power of relationship queries | Zaid |
| 5:00 | Enter MCP: Bridging AI and Databases | Jose Luis |
| 6:00 | Architecture overview diagram | Jose Luis |

**Zaid's Talking Points:**
- Traditional databases vs. graph databases for relationship queries
- "In crime investigation, relationships ARE the data"
- Example: "Find everyone connected to suspect X within 2 hops"

**Jose Luis's Talking Points:**
- MCP enables AI agents to discover and use tools dynamically
- "Instead of hardcoding database queries, the agent learns available capabilities"
- Architecture slide: User → Agent → MCP → Neo4j

---

### Part 2: Meet Holmsworth Marot (3 minutes)

| Time | Activity | Presenter |
|------|----------|-----------|
| 7:00 | Introduce the detective personality | Jose Luis |
| 8:00 | The crime database schema | Zaid |
| 9:00 | Quick Neo4j Browser tour | Zaid |

**Key Points:**
- Show the detective's personality system prompt
- Zaid demonstrates the crime graph in Neo4j Browser
- Visual: Show suspects, crimes, relationships

---

### Part 3: Live Demo - Investigation (12 minutes)

| Time | Activity | Presenter |
|------|----------|-----------|
| 10:00 | Start the application | Jose Luis |
| 11:00 | First query: "Show unsolved murders" | Jose Luis |
| 12:30 | Explain the MCP tool call | Jose Luis |
| 13:30 | Show the Cypher query generated | Zaid |
| 15:00 | Second query: "Who knows suspect X?" | Jose Luis |
| 16:30 | Graph traversal explanation | Zaid |
| 18:00 | Third query: Pattern detection | Jose Luis |
| 20:00 | Show graph visualization | Zaid |
| 21:00 | Interactive audience question (if time) | Both |

**Demo Script:**

```
Query 1: "Show me all unsolved murders in the database"
→ Jose runs, explains MCP flow
→ Zaid explains the Cypher: MATCH (c:Crime {status:'unsolved', type:'murder'})...

Query 2: "Who are the known associates of [suspect name]?"
→ Jose runs, shows relationship discovery
→ Zaid explains: This is WHERE graph shines - traversing relationships!

Query 3: "What patterns do you see in crimes near the waterfront?"
→ Jose shows AI reasoning capabilities
→ Zaid: Show location-based graph visualization in Browser
```

**Pro Tips:**
- Have backup screenshots in case of demo issues
- Pre-test the Neo4j connection before the session
- Keep conversation history for continuity

---

### Part 4: Technical Deep Dive (5 minutes)

| Time | Activity | Presenter |
|------|----------|-----------|
| 22:00 | MCP integration code walkthrough | Jose Luis |
| 24:00 | Neo4j MCP server capabilities | Zaid |
| 26:00 | Agent personality implementation | Jose Luis |

**Code Highlights:**

```csharp
// Jose: Show MCP client creation
await using var mcpClient = await Neo4jMcpClientFactory.CreateAsync(neo4jConfig);
var mcpTools = await mcpClient.ListToolsAsync();

// Jose: Show agent creation with tools
var detective = GraphDatabaseDetectiveAgent.Create([.. mcpTools.Cast<AITool>()]);
```

**Zaid's Points:**
- mcp-neo4j-cypher provides read_neo4j_cypher tool
- Safety: Read-only queries by default
- Schema introspection for AI understanding

---

### Part 5: Key Takeaways & Q&A (3 minutes)

| Time | Activity | Presenter |
|------|----------|-----------|
| 27:00 | Summary of key patterns | Jose Luis |
| 28:00 | When to use this approach | Zaid |
| 29:00 | Resources & next steps | Both |
| 29:30 | Q&A | Both |

**Key Takeaways Slide:**

1. ✅ **MCP enables dynamic tool discovery** - AI agents learn available capabilities
2. ✅ **Graph databases excel at relationships** - Perfect for investigation scenarios
3. ✅ **Personality enhances UX** - Detective character makes demo engaging
4. ✅ **Environment configuration** - Secure credential management
5. ✅ **Collaborative power** - AI reasoning + Graph traversal = Powerful insights

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
