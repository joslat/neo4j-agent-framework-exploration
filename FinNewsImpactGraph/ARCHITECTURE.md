# Finance Neo4j Demo — Architecture & Runbook

Date: 2025-12-18

## 1) Purpose

This repo is a small, local development prototype for modeling “financial contagion” using a Neo4j graph:

- **Entities**: companies, markets, countries, sectors, news events
- **Relationships**: ownership, partnerships, supply chain links, listings, HQ/operations footprints
- **Demo analysis**: propagate a negative news shock across the graph and rank potentially impacted companies

It is intentionally minimal and optimized for **local iteration** (Docker + a .NET console app), not production hardening.

## 2) High-level architecture

### Components

1. **Neo4j database (Docker container)**
   - Runs Neo4j Community (`neo4j:5.26-community`)
   - Exposes:
     - Browser UI: `http://localhost:7474`
     - Bolt (drivers): `neo4j://localhost:7687`
   - Optional plugin: **APOC** (enabled)

2. **Seed data (Cypher)**
   - [`neo4j/import/seed.cypher`](neo4j/import/seed.cypher)
   - Creates constraints + MERGE-based sample dataset and sample news events

3. **.NET 10 demo client**
  - [`src/FinNewsImpactGraphDemo/`](src/FinNewsImpactGraphDemo/)
   - Uses `Neo4j.Driver` to connect over Bolt
   - Runs:
     - a **seed routine** on startup (idempotent)
     - a **risk propagation** query
     - a **quick stats** query
   - Writes a local audit trail: `query-audit.jsonl`

### Data/control flow

1. Developer starts Neo4j via Docker Compose.
2. Data is seeded either:
   - manually via Browser using `:source /import/seed.cypher`, **or**
   - automatically by the demo app on startup (built-in seeding).
3. Demo app connects via Bolt, runs Cypher queries, prints results.
4. Developer explores the graph in Browser (optional), and can review `query-audit.jsonl` for traceability.

## 3) Container configuration

Source: [`docker-compose.yml`](docker-compose.yml)

- Service name: `neo4j`
- Container name: `neo4j-finance`
- Ports:
  - `7474:7474` (Neo4j Browser HTTP)
  - `7687:7687` (Bolt)
- Key environment variables:
  - `NEO4J_AUTH=neo4j/<password>`
  - `NEO4J_PLUGINS=["apoc"]`
  - `NEO4J_dbms_security_procedures_unrestricted=apoc.*`
  - `NEO4J_dbms_security_procedures_allowlist=apoc.*`
  - `NEO4J_server_directories_import=/import`
  - `NEO4J_dbms_security_allow__csv__import__from__file__urls=true`
  - Memory sizing defaults (heap/pagecache)

### Persistence model

The container uses **bind mounts** into `./neo4j/*`:

- `./neo4j/data` → database store
- `./neo4j/logs` → logs
- `./neo4j/conf` → config
- `./neo4j/import` → import files (including `seed.cypher`)
- `./neo4j/plugins` → plugins (APOC jar)

This means data persists across `docker compose down` unless you delete the local `neo4j/data` folder.

## 4) Data model (domain)

### Node labels

- `Company` (key: `ticker`)
- `Country` (key: `code`)
- `Market` (key: `code`)
- `Sector` (key: `name`)
- `NewsEvent` (key: `id`)

### Relationship types

- `(:Company)-[:LISTED_ON]->(:Market)`
- `(:Company)-[:HQ_IN]->(:Country)`
- `(:Company)-[:OPERATES_IN]->(:Country)`
- `(:Company)-[:IN_SECTOR]->(:Sector)`
- `(:Company)-[:OWNS {pct}]->(:Company)`
- `(:Company)-[:SUPPLIES_TO {criticality}]->(:Company)`
- `(:Company)-[:PARTNERS_WITH {strength}]->(:Company)`
- `(:NewsEvent)-[:AFFECTS {directImpact}]->(:Company)`

### Demo analytics model (toy)

The demo computes a simple “risk score” by:

- starting at a `NewsEvent` with `sentiment < 0`
- taking the `AFFECTS.directImpact` as the initial impact
- walking paths up to 2 hops across `OWNS|SUPPLIES_TO|PARTNERS_WITH`
- multiplying weights derived from relationship properties (`pct`, `criticality`, `strength`) with a fallback default

This is a prototype scoring function, not a calibrated financial model.

## 5) Application design (.NET)

Source: [`src/FinNewsImpactGraphDemo/FinNewsImpactGraphDemo.csproj`](src/FinNewsImpactGraphDemo/FinNewsImpactGraphDemo.csproj)

- Target framework: `net10.0`
- Driver: `Neo4j.Driver` `5.28.4`

### Runtime configuration

The demo reads environment variables:

- `NEO4J_URI` (default `neo4j://localhost:7687`)
- `NEO4J_USER` (default `neo4j`)
- `NEO4J_PASSWORD` (default `neo4j-password-change-me`)
- `NEWS_ID` (optional; defaults to `N-2025-12-18-001`)

### Observability / audit

The demo writes executed queries to:

- `query-audit.jsonl` (newline-delimited JSON)

This is intentionally simple and local.

## 6) Seeding process (answering: “Is there a data seeding process?”)

Yes. There are **two** ways to seed, and both are **idempotent** (they use constraints + `MERGE`, so rerunning is safe).

### Option A — Manual seed (recommended for “see what’s happening”)

- File: [`neo4j/import/seed.cypher`](neo4j/import/seed.cypher)
- Run it in Neo4j Browser:
  - `:source /import/seed.cypher`

### Option B — Automatic seed (recommended for “just run the demo”)

- The .NET demo app calls a seed routine on startup (creates constraints + merges nodes/relationships).
- This means a fresh machine can do a single `docker compose up -d` then `dotnet run` and the graph appears.

If you use both A and B, it’s fine (MERGE-based), but you’ll do redundant work.

## 7) Proper runbook (start → configure → seed → run demo)

### Prerequisites

- Docker Desktop is running
- .NET SDK 10 installed
- Ports `7474` and `7687` are free on localhost

### Step 1 — Configure credentials (recommended)

In [`docker-compose.yml`](docker-compose.yml), change:

- `NEO4J_AUTH=neo4j/neo4j-password-change-me`

Pick a dev password you’re comfortable with.

### Step 2 — Start the container

From the repo root:

- `docker compose up -d`

Verify:

- `docker ps` shows `neo4j-finance`
- Browser opens: `http://localhost:7474`

Optional readiness check (CLI):

- `docker exec neo4j-finance cypher-shell -u neo4j -p <password> "RETURN 1 AS ok;"`

### Step 3 — Seed data

Choose **one**:

**A) Browser seed**

1. Open `http://localhost:7474`
2. Login:
   - user: `neo4j`
   - password: your compose password
3. Run:
   - `:source /import/seed.cypher`

**B) App seed**

Just run the app (Step 4). It seeds automatically.

### Step 4 — Run the .NET demo

From repo root (PowerShell):

- `$env:NEO4J_URI="neo4j://localhost:7687"`
- `$env:NEO4J_USER="neo4j"`
- `$env:NEO4J_PASSWORD="<password from compose>"`
- `dotnet run --project .\src\FinNewsImpactGraphDemo\FinNewsImpactGraphDemo.csproj`

Optional: pick a different event:

- `$env:NEWS_ID="N-2025-12-18-002"`

Expected output:

- Connects successfully
- Prints ranked risk results (risk propagation)
- Prints `nodes=<n> rels=<m>`
- Writes `query-audit.jsonl`

### Step 5 — Validate in Neo4j Browser

Examples:

- View the shock + directly affected company:
  - `MATCH (n:NewsEvent {id:'N-2025-12-18-001'})-[:AFFECTS]->(c) RETURN n,c`

- Visualize the 0..2 hop neighborhood from VOLT:
  - `MATCH p=(c0:Company {ticker:'VOLT'})-[r:OWNS|PARTNERS_WITH|SUPPLIES_TO*0..2]-(c:Company) RETURN p`

### Step 6 — “Proper run” checklist (to avoid confusing failures)

- Password in compose matches the one you pass to the app (`NEO4J_PASSWORD`).
- Neo4j is fully started before running the app (check Browser opens or use `cypher-shell RETURN 1`).
- If you changed password after first startup, you must reset data (see reset section) or update credentials appropriately.
- If the graph looks empty:
  - run `:source /import/seed.cypher` manually, or
  - rerun the app (it seeds on startup)

## 8) Reset / cleanup

### Stop the container

- `docker compose down`

### Wipe data (start fresh)

Because persistence is a bind mount, you need to delete local data:

1. `docker compose down`
2. Delete:
   - `neo4j/data`

Then:

- `docker compose up -d`

## 9) Security notes (dev vs prod)

- Do not reuse the demo password for real workloads.
- `apoc.*` unrestricted procedures are convenient in dev but should be reviewed/locked down for production.
- Neo4j Community is great for prototyping; production deployments typically add backups, monitoring, auth hardening, and environment-specific configuration.

## 10) Next suggested evolutions

- Add explicit “shock” nodes and time-series ingestion (news → entities → markets).
- Add a proper scoring model: decay by hop distance, edge-type priors, sentiment confidence, and time decay.
- Add repeatable seeding as a command (`dotnet run -- --seed-only`), plus a migration strategy.
- Add dashboards (Grafana/Prometheus) if you move beyond local dev.
