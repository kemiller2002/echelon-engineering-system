# Indy Init rehearsal evidence

Evidence reviewed 2026-10-27. Statements below deliberately separate direct
observations from interpretation and hypotheses.

## Rehearsal 4

Source revision:
[`b6947b8f69ee16e9e8e10f706b15bd1c4d22f9b1`](https://github.com/kemiller2002/indy-init-rehearsal-004/tree/b6947b8f69ee16e9e8e10f706b15bd1c4d22f9b1).
The deployed candidate recorded by the run was
`ab08b764daddde9e978a78fef957d9ae01505c6a`.

### Observed

- The independent verifier computed `FAILED`; the report says `NOT ACCEPTED`.
- The run began at 2026-10-27 06:04:39 UTC and the verdict was produced at
  11:00:31 UTC: 296 of the 420 available minutes.
- Of 108 mandatory P0 requirements, 1 was `PASS`, 9 were `FAIL`, and 98 were
  `NOT_RUN`.
- Gates G0, G3, and G9 passed; G5 and G8 failed; G1, G2, G4, G6, and G7 were
  not run.
- The candidate evidence ledger found 160 checks: 156 passed and 4 failed.
- Six defect classes passed and four were not run.
- The live core path—create, add evidence and a hypothesis, record causal
  relationships, guarded save, reload, and conflict refusal—was exercised.
- `D-HIST-WASM` remained open: each historical case could load and replay
  separately, but loading all three together failed.
- Total tokens and cost were not measured. The report explicitly prohibits a
  token-efficiency comparison.
- A token entered a failed Playwright trace during the run. The owner rotated
  it, deleted the affected trace artifacts, and the upload cause was corrected.

Primary records:
[report](https://github.com/kemiller2002/indy-init-rehearsal-004/blob/b6947b8f69ee16e9e8e10f706b15bd1c4d22f9b1/docs/rehearsal/REHEARSAL-004-REPORT.md),
[verdict](https://github.com/kemiller2002/indy-init-rehearsal-004/blob/b6947b8f69ee16e9e8e10f706b15bd1c4d22f9b1/evidence/acceptance/verdict-ab08b764daddde9e978a78fef957d9ae01505c6a.json),
[ledger](https://github.com/kemiller2002/indy-init-rehearsal-004/blob/b6947b8f69ee16e9e8e10f706b15bd1c4d22f9b1/evidence/acceptance/ledger-ab08b764daddde9e978a78fef957d9ae01505c6a.json),
and [clock](https://github.com/kemiller2002/indy-init-rehearsal-004/blob/b6947b8f69ee16e9e8e10f706b15bd1c4d22f9b1/evidence/rehearsal/clock.json).

### Interpreted

- The evidence pipeline produced a stricter and more inspectable result than a
  self-declared acceptance status because the verdict was computed from a
  revision-bound ledger and evidence manifest.
- The run demonstrated the core write path but did not demonstrate competition
  readiness: the result and the remaining P0 evidence gap point in different
  directions and both must be retained.
- CI-only WebAssembly diagnosis and late acceptance-format integration were the
  largest documented process bottlenecks.

### Hypothesized

- Making WebAssembly builds reproducible locally may reduce CI round trips.
- Proving the acceptance schema and mapping every P0 requirement to a precise
  check in the first hour may reduce late evidence work.
- Those changes have not yet been demonstrated by a comparable accepted run.

## Rehearsal 5

Source revision:
[`52e9a74c870804eb72936439fbf79c651ef348c1`](https://github.com/kemiller2002/indy-init-rehearsal-005/tree/52e9a74c870804eb72936439fbf79c651ef348c1).

### Observed

The repository contains only an initial commit and a one-line README at this
revision. It publishes no objective, protocol, run clock, acceptance ledger,
test result, token measurement, report, or verdict.

### Interpretation

Rehearsal 5 is `NOT RUN` for this website's comparison purposes. Missing
evidence is not a zero and does not establish failure or success.

### Hypothesis

A compatible Rehearsal 5 report may support comparison later. Until it exists,
the website will not publish cross-rehearsal performance conclusions.
