# Engineering System Website: Requirements v0.1

Status: DRAFT. Requirements are specifications, not implementation claims.
Target: Indy Init demonstration, November 7, 2026.
Repository: kemiller2002/echelon-engineering-system

## Purpose
Explain Echelon's engineering system to technical and nontechnical visitors using accessible, evidence-backed diagrams and guided narratives. Do not confuse planned integrations with demonstrated behavior.

## Mandatory discovery before visual implementation
- REQ-BRAND-001: Identify the **actual main Echelon Foundry website** and its source repository; record URL, commit, design tokens, typography, colors, spacing, navigation, and component patterns in a provenance document. Do not treat EchoWorks marketing as the Echelon brand: its README explicitly says it uses a distinct identity.
- REQ-BRAND-002: Reuse the main Echelon site's design language and approved assets; do not invent a second brand or silently copy EchoWorks styling.
- REQ-BRAND-003: Use Forma components and shared tokens wherever available. Document any unavoidable deviations with evidence.
- REQ-BRAND-004: Validate contrast, focus states, text wrapping, and responsive layout against the source design and accessibility requirements.

## Runtime and architecture
- REQ-TECH-001: Application code is F#; use Limen for browser/WebAssembly application infrastructure and Forma for UI. Use Folio where printable documentation is needed. No Bolero by default.
- REQ-TECH-002: Verify actual Limen capabilities and dependencies before committing to runtime assumptions. No undocumented Blazor, JavaScript framework, or alternative UI framework substitutions.
- REQ-TECH-003: Publish to GitHub Pages with relative paths and deterministic, pinned builds. Static content and essential documentation must remain readable if WebAssembly fails to load.
- REQ-TECH-004: Maintain source documents as versioned Markdown and structured manifests. Do not introduce a second authoritative copy of component contracts.

## Information architecture
- REQ-CONTENT-001: Home page: concise thesis, system overview, guided five-minute walkthrough, navigation to evidence.
- REQ-CONTENT-002: Architecture: component inventory, roles, input/output contracts, dependency graph, source repository links.
- REQ-CONTENT-003: Engineering workflow: requirement intake, compilation, work planning, implementation, verification, completion gates, provenance.
- REQ-CONTENT-004: Agent decisioning: explicitly distinguish predetermined constraints, delegated choices, forbidden transitions, and human approvals.
- REQ-CONTENT-005: Four-tier engineering process: derive exact tier definitions from Ordo source; no inferred labels.
- REQ-CONTENT-006: Research: hypothesis, method, controls, measurements, failures, limitations, reproducible evidence.
- REQ-CONTENT-007: Indy Init: rehearsal-by-rehearsal timeline, versioned results, known blockers, and links to evidence.
- REQ-CONTENT-008: Every architectural claim must be marked implemented, verified, experimental, planned, or unknown, with source and as-of revision.

## Diagrams and interaction
- REQ-DIAG-001: Provide system context, component dependency, agent decision boundary, work-item lifecycle, and Indy Init execution flow diagrams.
- REQ-DIAG-002: Diagram source must be version-controlled and reproducibly render to SVG; accessible textual equivalents are required.
- REQ-DIAG-003: Support keyboard-only navigation, visible focus, meaningful accessible names, reduced motion, zoom, and touch targets.
- REQ-DIAG-004: No clipping or escaped text in cards, rounded panels, labels, and diagrams at 320, 390, 768, 1280px and 200% text scaling. Test long names and adversarial content.
- REQ-DIAG-005: Interactive states must never be the sole way to access information.

## Evidence and governance
- REQ-EVID-001: Treat Praxis, Ordo, Dokimos, Causa, and each component repository as authoritative for their own documented contracts and experiment outputs.
- REQ-EVID-002: Verify repository names, branches, commits, and paths before linking. Do not fabricate metrics or results.
- REQ-EVID-003: Show negative outcomes and blocked gates alongside successes. Separate interpretation from observation.
- REQ-EVID-004: Never publish credentials, private logs, personal information, or unreviewed internal data.
- REQ-GOV-001: Follow the repository's Praxis intake conventions after inspecting the actual documented path; do not create GitHub issues as a substitute.

## Acceptance gates
- GATE-01: Source-of-truth brand identified and approved; visual parity review completed.
- GATE-02: Five-minute walkthrough can be completed with keyboard alone and with WebAssembly unavailable.
- GATE-03: Each component and result has traceable evidence or an explicit unknown/planned label.
- GATE-04: Browser layout checks pass at listed widths, text zoom, and reduced motion.
- GATE-05: F# build, unit tests, accessibility tests, and Pages deployment checks pass.
- GATE-06: No secrets or unsupported claims are published.

## Implementation sequence
1. Inventory source repositories and discover the main Echelon website; record source commits.
2. Confirm Limen/Forma integration capabilities and deployment strategy.
3. Produce sitemap, design token mapping, and diagram manifests.
4. Implement accessible static-first content and the five-minute walkthrough.
5. Add evidence-backed rehearsal views and validate all gates.

No implementation is accepted solely because this requirements document exists.
