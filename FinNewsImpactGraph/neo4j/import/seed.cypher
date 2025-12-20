// Finance/market graph seed (safe to re-run)
// Run in Neo4j Browser or via cypher-shell.
// Note: This is a DEMO dataset. Relationship weights (pct/strength/criticality)
// are illustrative for propagation behavior and are not intended as factual measurements.

// Constraints
CREATE CONSTRAINT company_ticker IF NOT EXISTS FOR (c:Company) REQUIRE c.ticker IS UNIQUE;
CREATE CONSTRAINT country_code IF NOT EXISTS FOR (c:Country) REQUIRE c.code IS UNIQUE;
CREATE CONSTRAINT market_code IF NOT EXISTS FOR (m:Market) REQUIRE m.code IS UNIQUE;
CREATE CONSTRAINT sector_name IF NOT EXISTS FOR (s:Sector) REQUIRE s.name IS UNIQUE;
CREATE CONSTRAINT news_id IF NOT EXISTS FOR (n:NewsEvent) REQUIRE n.id IS UNIQUE;

// Countries
MERGE (us:Country {code:'US', name:'United States'})
MERGE (tw:Country {code:'TW', name:'Taiwan'})
MERGE (nl:Country {code:'NL', name:'Netherlands'})
MERGE (kr:Country {code:'KR', name:'South Korea'})
MERGE (cn:Country {code:'CN', name:'China'})

// Markets
MERGE (nasdaq:Market {code:'NASDAQ', name:'NASDAQ'})
MERGE (nyse:Market {code:'NYSE', name:'New York Stock Exchange'})

// Sectors
MERGE (cloud:Sector {name:'Cloud'})
MERGE (ai:Sector {name:'AI'})
MERGE (semis:Sector {name:'Semiconductors'})
MERGE (tools:Sector {name:'Developer Tools'})
MERGE (internet:Sector {name:'Internet Platforms'})
MERGE (hardware:Sector {name:'Hardware'})

// Companies (real-world org names; demo relationships/weights)
// Note: `ticker` is used here as a unique identifier; some entities are not publicly traded.
MERGE (msft:Company {ticker:'MSFT', name:'Microsoft'})
  SET msft.riskProfile='cloud-platform'
MERGE (openai:Company {ticker:'OPENAI', name:'OpenAI'})
  SET openai.riskProfile='model-provider'
MERGE (nvda:Company {ticker:'NVDA', name:'NVIDIA'})
  SET nvda.riskProfile='compute-supply'
MERGE (tsmc:Company {ticker:'TSMC', name:'TSMC'})
  SET tsmc.riskProfile='foundry'
MERGE (asml:Company {ticker:'ASML', name:'ASML'})
  SET asml.riskProfile='fab-equipment'
MERGE (amd:Company {ticker:'AMD', name:'AMD'})
  SET amd.riskProfile='compute-competitor'
MERGE (intc:Company {ticker:'INTC', name:'Intel'})
  SET intc.riskProfile='compute-competitor'
MERGE (github:Company {ticker:'GITHUB', name:'GitHub'})
  SET github.riskProfile='developer-platform'

// Additional cloud / AI ecosystem (real org names; demo relationships/weights)
MERGE (amzn:Company {ticker:'AMZN', name:'Amazon'})
  SET amzn.riskProfile='cloud-platform'
MERGE (aws:Company {ticker:'AWS', name:'Amazon Web Services'})
  SET aws.riskProfile='cloud-platform'

MERGE (googl:Company {ticker:'GOOGL', name:'Alphabet'})
  SET googl.riskProfile='internet-platform'
MERGE (gcp:Company {ticker:'GCP', name:'Google Cloud'})
  SET gcp.riskProfile='cloud-platform'

MERGE (meta:Company {ticker:'META', name:'Meta'})
  SET meta.riskProfile='internet-platform'
MERGE (aapl:Company {ticker:'AAPL', name:'Apple'})
  SET aapl.riskProfile='device-platform'

MERGE (tsla:Company {ticker:'TSLA', name:'Tesla'})
  SET tsla.riskProfile='ai-enabled-hardware'
MERGE (x:Company {ticker:'X', name:'X'})
  SET x.riskProfile='internet-platform'
MERGE (xai:Company {ticker:'XAI', name:'xAI'})
  SET xai.riskProfile='model-provider'

MERGE (anthropic:Company {ticker:'ANTHROPIC', name:'Anthropic'})
  SET anthropic.riskProfile='model-provider'
MERGE (pplx:Company {ticker:'PPLX', name:'Perplexity'})
  SET pplx.riskProfile='ai-product'
MERGE (cohere:Company {ticker:'COHERE', name:'Cohere'})
  SET cohere.riskProfile='model-provider'
MERGE (mistral:Company {ticker:'MISTRAL', name:'Mistral AI'})
  SET mistral.riskProfile='model-provider'

// Additional semis / enabling hardware
MERGE (mu:Company {ticker:'MU', name:'Micron Technology'})
  SET mu.riskProfile='memory-supply'
MERGE (avgo:Company {ticker:'AVGO', name:'Broadcom'})
  SET avgo.riskProfile='networking-supply'
MERGE (qcom:Company {ticker:'QCOM', name:'Qualcomm'})
  SET qcom.riskProfile='edge-ai'
MERGE (arm:Company {ticker:'ARM', name:'Arm'})
  SET arm.riskProfile='cpu-ip'
MERGE (smsn:Company {ticker:'SMSN', name:'Samsung Electronics'})
  SET smsn.riskProfile='memory-supply'
MERGE (skh:Company {ticker:'SKH', name:'SK hynix'})
  SET skh.riskProfile='memory-supply'
MERGE (orcl:Company {ticker:'ORCL', name:'Oracle'})
  SET orcl.riskProfile='cloud-platform'

// Listings, HQ, Sector
MERGE (msft)-[:LISTED_ON]->(nasdaq)
MERGE (nvda)-[:LISTED_ON]->(nasdaq)
MERGE (amd)-[:LISTED_ON]->(nasdaq)
MERGE (intc)-[:LISTED_ON]->(nasdaq)
MERGE (asml)-[:LISTED_ON]->(nasdaq)
MERGE (tsmc)-[:LISTED_ON]->(nyse)

MERGE (amzn)-[:LISTED_ON]->(nasdaq)
MERGE (googl)-[:LISTED_ON]->(nasdaq)
MERGE (meta)-[:LISTED_ON]->(nasdaq)
MERGE (aapl)-[:LISTED_ON]->(nasdaq)
MERGE (tsla)-[:LISTED_ON]->(nasdaq)
MERGE (mu)-[:LISTED_ON]->(nasdaq)
MERGE (avgo)-[:LISTED_ON]->(nasdaq)
MERGE (qcom)-[:LISTED_ON]->(nasdaq)
MERGE (arm)-[:LISTED_ON]->(nasdaq)
MERGE (orcl)-[:LISTED_ON]->(nyse)

MERGE (msft)-[:HQ_IN]->(us)
MERGE (openai)-[:HQ_IN]->(us)
MERGE (nvda)-[:HQ_IN]->(us)
MERGE (amd)-[:HQ_IN]->(us)
MERGE (intc)-[:HQ_IN]->(us)
MERGE (github)-[:HQ_IN]->(us)
MERGE (tsmc)-[:HQ_IN]->(tw)
MERGE (asml)-[:HQ_IN]->(nl)

MERGE (amzn)-[:HQ_IN]->(us)
MERGE (aws)-[:HQ_IN]->(us)
MERGE (googl)-[:HQ_IN]->(us)
MERGE (gcp)-[:HQ_IN]->(us)
MERGE (meta)-[:HQ_IN]->(us)
MERGE (aapl)-[:HQ_IN]->(us)
MERGE (tsla)-[:HQ_IN]->(us)
MERGE (x)-[:HQ_IN]->(us)
MERGE (xai)-[:HQ_IN]->(us)
MERGE (anthropic)-[:HQ_IN]->(us)
MERGE (pplx)-[:HQ_IN]->(us)
MERGE (cohere)-[:HQ_IN]->(us)
MERGE (mistral)-[:HQ_IN]->(us)
MERGE (mu)-[:HQ_IN]->(us)
MERGE (avgo)-[:HQ_IN]->(us)
MERGE (qcom)-[:HQ_IN]->(us)
MERGE (arm)-[:HQ_IN]->(us)
MERGE (orcl)-[:HQ_IN]->(us)
MERGE (smsn)-[:HQ_IN]->(kr)
MERGE (skh)-[:HQ_IN]->(kr)

MERGE (msft)-[:IN_SECTOR]->(cloud)
MERGE (openai)-[:IN_SECTOR]->(ai)
MERGE (nvda)-[:IN_SECTOR]->(semis)
MERGE (tsmc)-[:IN_SECTOR]->(semis)
MERGE (asml)-[:IN_SECTOR]->(semis)
MERGE (amd)-[:IN_SECTOR]->(semis)
MERGE (intc)-[:IN_SECTOR]->(semis)
MERGE (github)-[:IN_SECTOR]->(tools)

MERGE (amzn)-[:IN_SECTOR]->(internet)
MERGE (aws)-[:IN_SECTOR]->(cloud)
MERGE (googl)-[:IN_SECTOR]->(internet)
MERGE (gcp)-[:IN_SECTOR]->(cloud)
MERGE (meta)-[:IN_SECTOR]->(internet)
MERGE (aapl)-[:IN_SECTOR]->(hardware)
MERGE (tsla)-[:IN_SECTOR]->(hardware)
MERGE (x)-[:IN_SECTOR]->(internet)
MERGE (xai)-[:IN_SECTOR]->(ai)
MERGE (anthropic)-[:IN_SECTOR]->(ai)
MERGE (pplx)-[:IN_SECTOR]->(ai)
MERGE (cohere)-[:IN_SECTOR]->(ai)
MERGE (mistral)-[:IN_SECTOR]->(ai)
MERGE (mu)-[:IN_SECTOR]->(semis)
MERGE (avgo)-[:IN_SECTOR]->(semis)
MERGE (qcom)-[:IN_SECTOR]->(semis)
MERGE (arm)-[:IN_SECTOR]->(semis)
MERGE (orcl)-[:IN_SECTOR]->(cloud)
MERGE (smsn)-[:IN_SECTOR]->(semis)
MERGE (skh)-[:IN_SECTOR]->(semis)

// Operations footprint (illustrative)
MERGE (msft)-[:OPERATES_IN]->(cn)
MERGE (nvda)-[:OPERATES_IN]->(cn)
MERGE (tsmc)-[:OPERATES_IN]->(cn)

// Ownership structure (safe, widely-known acquisition)
MERGE (msft)-[:OWNS {pct:100.0}]->(github)

// Internal business-unit modeling (illustrative: treat as owned subsidiaries)
MERGE (amzn)-[:OWNS {pct:100.0}]->(aws)
MERGE (googl)-[:OWNS {pct:100.0}]->(gcp)

// Partnerships / supply chain (illustrative weights)
MERGE (msft)-[:PARTNERS_WITH {strength:0.85}]->(openai)
MERGE (msft)-[:SUPPLIES_TO {criticality:0.75}]->(openai)

MERGE (nvda)-[:SUPPLIES_TO {criticality:0.8}]->(msft)
MERGE (amd)-[:SUPPLIES_TO {criticality:0.35}]->(msft)

// Competitive cloud branch
MERGE (nvda)-[:SUPPLIES_TO {criticality:0.75}]->(aws)
MERGE (nvda)-[:SUPPLIES_TO {criticality:0.7}]->(gcp)
MERGE (nvda)-[:SUPPLIES_TO {criticality:0.55}]->(meta)
MERGE (amd)-[:SUPPLIES_TO {criticality:0.25}]->(aws)
MERGE (intc)-[:SUPPLIES_TO {criticality:0.2}]->(aws)
MERGE (avgo)-[:SUPPLIES_TO {criticality:0.45}]->(aws)
MERGE (avgo)-[:SUPPLIES_TO {criticality:0.35}]->(gcp)

// Model providers / products and their infra relationships (illustrative)
MERGE (aws)-[:SUPPLIES_TO {criticality:0.7}]->(anthropic)
MERGE (aws)-[:PARTNERS_WITH {strength:0.6}]->(anthropic)
MERGE (gcp)-[:SUPPLIES_TO {criticality:0.5}]->(cohere)
MERGE (gcp)-[:PARTNERS_WITH {strength:0.45}]->(cohere)
MERGE (aws)-[:SUPPLIES_TO {criticality:0.4}]->(pplx)
MERGE (msft)-[:SUPPLIES_TO {criticality:0.35}]->(pplx)

// xAI / X / Tesla (illustrative)
MERGE (xai)-[:PARTNERS_WITH {strength:0.45}]->(x)
MERGE (xai)-[:PARTNERS_WITH {strength:0.35}]->(tsla)

MERGE (tsmc)-[:SUPPLIES_TO {criticality:0.9}]->(nvda)
MERGE (tsmc)-[:SUPPLIES_TO {criticality:0.6}]->(amd)
MERGE (asml)-[:SUPPLIES_TO {criticality:0.7}]->(tsmc)

// Additional hardware upstream/downstream (illustrative)
MERGE (tsmc)-[:SUPPLIES_TO {criticality:0.4}]->(intc)
MERGE (asml)-[:SUPPLIES_TO {criticality:0.45}]->(intc)
MERGE (skh)-[:SUPPLIES_TO {criticality:0.5}]->(nvda)
MERGE (mu)-[:SUPPLIES_TO {criticality:0.45}]->(nvda)
MERGE (smsn)-[:SUPPLIES_TO {criticality:0.35}]->(nvda)
MERGE (arm)-[:SUPPLIES_TO {criticality:0.2}]->(aapl)
MERGE (tsmc)-[:SUPPLIES_TO {criticality:0.4}]->(aapl)
MERGE (nvda)-[:SUPPLIES_TO {criticality:0.35}]->(tsla)

// News events
// - Headlines/links can be taken from RSS feeds.
// - Summaries are original and short (do not paste article text).
// - Each event is connected to impacted companies via :AFFECTS.
// - Events are also tagged to a market via :ASSOCIATED_WITH for visualization.

// WSJ Markets RSS — AI/chips themed market move
MERGE (n1:NewsEvent {id:'N-2025-12-18-001'})
  SET n1.source='WSJ Markets RSS',
      n1.sourceFeed='https://feeds.content.dowjones.io/public/rss/RSSMarketsMain',
      n1.url='https://www.wsj.com/finance/stocks/ai-themed-stocks-take-more-hits-sending-nasdaq-lower?mod=rss_markets_main',
      n1.headline='AI-Themed Stocks Take More Hits, Sending Nasdaq Lower',
      n1.summary='A risk-off move in AI-linked equities can pressure both model builders and their compute supply chain, with second-order impacts on cloud spending and capex sentiment.',
      n1.sentiment=-0.45,
      n1.ts=datetime('2025-12-17T21:57:00Z')
MERGE (n1)-[:AFFECTS {directImpact:0.6}]->(nvda)
MERGE (n1)-[:AFFECTS {directImpact:0.35}]->(tsla)
MERGE (n1)-[:AFFECTS {directImpact:0.25}]->(msft)
MERGE (n1)-[:ASSOCIATED_WITH]->(nasdaq)

// WSJ Markets RSS — memory tightness narrative (feeds into AI hardware availability)
MERGE (n2:NewsEvent {id:'N-2025-12-18-002'})
  SET n2.source='WSJ Markets RSS',
      n2.sourceFeed='https://feeds.content.dowjones.io/public/rss/RSSMarketsMain',
      n2.url='https://www.wsj.com/tech/microns-blowout-results-are-bad-news-for-anyone-buying-a-new-phone-or-pc-next-year-1b303c09?mod=rss_markets_main',
      n2.headline='Micron’s Blowout Results Are Bad News for Anyone Buying a New Phone or PC Next Year',
      n2.summary='Strong AI-driven demand can tighten memory supply and raise costs, which may amplify compute constraints for GPU-heavy deployments and large training runs.',
      n2.sentiment=-0.35,
      n2.ts=datetime('2025-12-18T10:30:00Z')
MERGE (n2)-[:AFFECTS {directImpact:0.55}]->(mu)
MERGE (n2)-[:AFFECTS {directImpact:0.25}]->(nvda)
MERGE (n2)-[:ASSOCIATED_WITH]->(nasdaq)

// Bloomberg Markets RSS — enterprise demand / model provider commercialization
MERGE (n3:NewsEvent {id:'N-2025-12-18-003'})
  SET n3.source='Bloomberg Markets RSS',
      n3.sourceFeed='https://feeds.bloomberg.com/markets/news.rss',
      n3.url='https://www.bloomberg.com/news/articles/2025-12-19/inside-disney-and-openai-s-billion-dollar-deal-big-take-podcast',
      n3.headline='Inside Disney and OpenAI’s Billion Dollar Deal',
      n3.summary='Large enterprise AI deals can expand distribution and revenue expectations for model providers, but also raise delivery, cost, and governance requirements.',
      n3.sentiment=0.25,
      n3.ts=datetime('2025-12-19T22:53:12Z')
MERGE (n3)-[:AFFECTS {directImpact:0.5}]->(openai)
MERGE (n3)-[:AFFECTS {directImpact:0.2}]->(msft)
MERGE (n3)-[:ASSOCIATED_WITH]->(nasdaq)

// Bloomberg Markets RSS — hardware input constraint (illustrative propagation into hardware-heavy firms)
MERGE (n4:NewsEvent {id:'N-2025-12-18-004'})
  SET n4.source='Bloomberg Markets RSS',
      n4.sourceFeed='https://feeds.bloomberg.com/markets/news.rss',
      n4.url='https://www.bloomberg.com/news/articles/2025-12-20/china-s-rare-earth-magnet-exports-to-us-decline-in-november',
      n4.headline='China’s Rare-Earth Magnet Exports to US Decline in November',
      n4.summary='A squeeze in key industrial inputs can raise costs and extend lead times for hardware-intensive supply chains; the impact often shows up indirectly across dependent firms.',
      n4.sentiment=-0.25,
      n4.ts=datetime('2025-12-20T03:48:13Z')
MERGE (n4)-[:AFFECTS {directImpact:0.45}]->(tsla)
MERGE (n4)-[:AFFECTS {directImpact:0.15}]->(nvda)
MERGE (n4)-[:ASSOCIATED_WITH]->(nasdaq)

// Policy shock example (synthetic headline; do not treat as factual)
MERGE (n5:NewsEvent {id:'N-2025-12-18-005'})
  SET n5.source='Demo (synthetic)',
      n5.sourceFeed='(none)',
      n5.url='(none)',
      n5.headline='Proposed policy restrictions create uncertainty for advanced AI chip supply',
      n5.summary='Regulatory uncertainty can constrain near-term availability of advanced accelerators and raise compliance costs, with knock-on effects across cloud providers and model builders.',
      n5.sentiment=-0.55,
      n5.ts=datetime('2025-12-20T09:00:00Z')
MERGE (n5)-[:AFFECTS {directImpact:0.7}]->(nvda)
MERGE (n5)-[:AFFECTS {directImpact:0.4}]->(tsmc)
MERGE (n5)-[:AFFECTS {directImpact:0.25}]->(asml)
MERGE (n5)-[:ASSOCIATED_WITH]->(nasdaq)
