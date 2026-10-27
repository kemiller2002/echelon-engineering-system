# Detailed website requirements and source inventory v0.2

Status: DRAFT / source-verified where stated. Created 2026-10-27. No build or browser checks executed.

## Verified source inventory

| System | Repository | Evidence from README | Role |
| --- | --- | --- | --- |
| Forma | kemiller2002/forma | Shared Echelon design system; tokens, HTML/CSS, accessibility contracts; zero-runtime presentation | Canonical component and visual foundation |
| Limen | kemiller2002/limen | Browser kernel/protocol currently TypeScript; F#/.NET WASM consumers exist; WASM engine is not shipped in npm package | Optional interactive browser boundary |
| Ordo | kemiller2002/ordo | Canonical state-directed engineering methodology; F# native CLI | Process definitions |
| Visual Engineering | kemiller2002/visual-engineering | UI-LAYOUT-INTERACTION@1.0.0 and application-polish standards | Layout/interaction acceptance |
| Echelon Registry | kemiller2002/echelon-registry | Capability-based discovery and optional provider contract | System inventory semantics |
| Indy Init | kemiller2002/Indy-init | Hackathon experiment and Root-Cause Investigator requirements | Competition narrative |
| Personal site | kemiller2002/kemiller2002.github.io | F# static generator and Forma 0.3.0, no Limen by design | Static publishing reference, NOT confirmed corporate Echelon branding |
| EchoWorks marketing | kemiller2002/echo-works-marketing- | README explicitly disclaims Echelon visual identity | DO NOT copy brand |

Other repositories discovered by name, with contracts not yet verified: vigila, arca, strata, forma-studio, folio, summa, signal, echelon-diagnostic-framework, indy-init-rehearsal-001 through -005. Additional systems (Praxis, Dokimos, Tutors, Causa, Vitium, Aegis) require canonical-repository resolution; do not guess from historical names.

## Branding discovery gate

BR-01: Identify actual corporate Echelon website URL and authoritative repository or design source. Current status UNKNOWN.
BR-02: Inspect Forma's active tokens, release lock and approved brand variants. Use exact values only after source inspection.
BR-03: Record typography, color palette, contrast, page width, breakpoints, spacing scale, radius, shadows, navigation, iconography, hero and card patterns, focus behavior, dark mode and motion.
BR-04: Publish a versioned design-mapping document with file paths, commit hashes and screenshots of reference pages.
BR-05: No UI implementation is considered visually accepted until BR-01 through BR-04 are verified. An unverified site can still ship as a clearly labeled preview using Forma's baseline.

## Page-by-page functional specification

### / Home
HOME-01: Hero explains constraint-driven autonomous engineering in plain English, without unsupported claims.
HOME-02: Show five-stage overview and clear start for a five-minute guided tour.
HOME-03: Two visitor pathways: quick explanation and engineering deep dive.
HOME-04: Surface evidence/unknown status and last verified revision.
HOME-05: Every primary CTA has a working destination and meaningful focus state.

### /system/ Architecture atlas
SYS-01: Accessible system map organized into governance, execution, assurance, browser/presentation, product, and evidence; grouping must be source-verified.
SYS-02: Each node opens a semantic detail view with purpose, authoritative repository, version, inputs, outputs, optional dependencies, implementation status and evidence links.
SYS-03: Show directed dependencies with a text-list equivalent; do not infer actual integration from conceptual adjacency.
SYS-04: Provide filtered views by responsibility and a readable unfiltered view.
SYS-05: Explicit legend for verified implementation, documented contract, experiment, proposed and unknown.

### /process/ Engineering lifecycle
PROC-01: Walk requirement -> acceptance criteria -> work planning -> agent execution -> validation -> evidence -> completion gate.
PROC-02: Explain which system owns each transition based on its actual contract.
PROC-03: Illustrate rejected transitions and recovery, not just happy path.
PROC-04: Show the actual four-tier method after reading Ordo canonical sources; do not invent tier names.

### /agents/ Decision boundaries
AG-01: Distinguish human policy, fixed constraints, agent discretion, and external authorization.
AG-02: Show input/output payload shapes only from source-verified contracts.
AG-03: Present at least one example of an invalid decision blocked before execution and one verification failure; label examples simulated unless supported by real evidence.
AG-04: Explain how provenance, identity, and state persist across agent handoffs.

### /components/ Component directory
CMP-01: Searchable or browsable catalog with canonical repo, purpose, interface, maturity, dependencies and source commit.
CMP-02: Missing components remain visible as unknown/unverified rather than disappearing.
CMP-03: Avoid copying canonical API docs; link to them and show a brief explanatory summary.

### /research/ Experiments
RES-01: Show question, hypothesis, protocol, comparison/control, observed results, limitations, artifacts and next steps.
RES-02: Do not present token or correctness improvements without a reproducible measurement source.
RES-03: Allow comparisons across experiments only when metrics and definitions are compatible.

### /indy-init/ Rehearsal evidence
INDY-01: Show Rehearsals 1-5 with actual run IDs, revision, objectives, gates, outcomes, blockers and linked evidence when available.
INDY-02: Separate completed, running, not run and blocked states.
INDY-03: Provide a timeline from compiled requirements to verified product, preserving unresolved failures.
INDY-04: Avoid presenting the competition application as built during the event before evidence establishes that fact.

### /tour/ Guided walkthrough
TOUR-01: Five-minute route with 5-7 steps and optional deep dives.
TOUR-02: Each step includes a visual, an accessible narrative, and an evidence link.
TOUR-03: Keyboard, touch, reduced motion, screen reader and no-WASM paths must all reach equivalent content.
TOUR-04: Progress state must not require remote persistence.

### /reference/ Documentation and /about/ Method
REF-01: Versioned glossary, sources, methodology, limitations and contact/feedback destination.
REF-02: Clearly separate doctrine, architectural contract, implementation, and experimental evidence.

## Diagram specifications

D-01 System context: actor -> requirements -> governed engineering -> verified deliverable; text alternative.
D-02 Component map: directed dependencies with source revision and status legend.
D-03 State-transition map: permitted, denied and unresolved decisions.
D-04 Work-item sequence: handoffs and evidence ownership.
D-05 Rehearsal timeline: timestamps and gates from actual artifacts.
D-06 Four-tier engineering map: only after source verification.
D-07 Diagrams generated from versioned data and SVG where practical, with headings, labels, descriptive text, keyboard support and no essential hover-only data.
D-08 Long labels, 200% text, 320px viewport and high contrast must not clip within nodes or rounded containers.

## Technical and quality constraints

T-01: F# for application/domain/build logic. Static F# publishing is preferred for documentation; do not add WASM solely for branding or static diagrams.
T-02: Use Forma semantic patterns and CSS tokens. Limen only for stateful interactive surfaces when required. Limen's current TypeScript browser kernel is an acknowledged infrastructure dependency, not a license to add another JS framework.
T-03: No Bolero; deviations require written justification and approval.
T-04: GitHub Pages deployment, pinned dependencies, relative links, deterministic output and documented local build/test commands.
T-05: Essential content, navigation, diagrams and evidence links remain usable without WASM.
T-06: Follow Visual Engineering UI-LAYOUT-INTERACTION@1.0.0; browser-test 320/390/768/851/1000/1280px, 200% zoom, reduced motion, keyboard-only, long-label and empty/error states.
T-07: Unit tests for content schema, route/link validity, claim-to-source mapping and SVG text alternatives.
T-08: No unreviewed telemetry, credentials, private evidence or sensitive data in public artifacts.
T-09: Do not add a database for a read-only documentation site. Any later interactive data requirements need separate review.

## Next work packets (do not claim complete)

WP-01: Resolve corporate Echelon brand authority; collect tokens and examples.
WP-02: Verify Forma release and Limen capability boundaries against source files.
WP-03: Resolve canonical repository identities for Praxis, Dokimos, Tutors, Aegis, Vitium and Causa.
WP-04: Inspect Ordo's four-tier method and document verified definitions.
WP-05: Inventory actual rehearsal evidence and generate source-linked records.
WP-06: Produce F# static site skeleton and route tests after brand gate.
WP-07: Implement diagrams and walkthrough; run full visual conformance.

Acceptance: every requirement receives an evidence artifact and a pass/fail/not-run state. This document itself does not satisfy acceptance.
