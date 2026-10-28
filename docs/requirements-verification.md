# Engineering system website verification

Last updated: 2026-10-28. Gate state is evidence-driven; a commit by itself is
not acceptance. `PENDING` below is represented as `NOT RUN` until the named
external or clean-checkout check completes.

## Baseline and implementation

- Repository: `kemiller2002/echelon-engineering-system`
- Delivery branch and pull request: `codex/complete-engineering-system-site`,
  [#1](https://github.com/kemiller2002/echelon-engineering-system/pull/1)
- Starting revision: `99e4fcbd489fbd98ab897ae083034e7523e1456d`
- Deployed merge revision: `6849e0907e40cc46533689d3f25ddcf54f1a4da7`
- Baseline defect: the existing F# project failed with FS0010 in `Program.fs`.
- Dependency defects: Forma 0.9.1 did not exist in the package registry; Folio
  0.4.2 did not exist as a published release. The implementation pins the
  verified releases Forma 0.9.0 and Folio 0.4.1.
- Deployment configuration: GitHub Pages is configured for Actions at
  `https://engineering.echelonfoundry.com/`.
- Scope amendment: the user removed the standalone Indy Init page on
  2026-10-28. The route, navigation item, D05 diagram, and derived PDF are no
  longer part of the website; the source-backed rehearsal material remains in
  Research and the evidence records.

## Commands executed

| Check | Result |
|---|---|
| `npm run build` | PASS — .NET 10 build, zero warnings/errors; F# publisher generated and structurally validated 8 routes and 5 required assets. |
| `npm test` | PASS — 22 Playwright checks. |
| `npm run test:branding` | PASS — 5 checks against the built corporate reference at 390 and 1280 px. |
| `npm run test:print` | PASS — 5 checks; four tagged US Letter PDFs generated. |
| Poppler `pdfinfo`, `pdftoppm`, and visual page inspection | PASS — the 15 retained rendered pages were reviewed; tables, diagrams, headings, page breaks, and grayscale/background-disabled output are readable. |
| `git diff --check` | PASS. |
| Fresh-checkout `npm ci`, build, and tests | PASS — pushed commit `ce41cc4` was cloned into a new directory; exact dependencies installed and all 22 tests passed. |
| Production URL inspection | PASS — Pages workflow [40547706606](https://github.com/kemiller2002/echelon-engineering-system/actions/runs/40547706606) built and deployed merge `6849e09`; 5 live Chromium checks passed at `https://engineering.echelonfoundry.com/`. |

## Browser and accessibility coverage

- All eight routes at widths 320, 390, 768, 851, 1000, and 1280 px.
- No page-level horizontal overflow.
- 200% text scaling, reduced motion, keyboard skip navigation, visible focus,
  semantic landmarks, exactly one H1, and current-page navigation.
- Adversarial unbroken component names and URLs.
- Content remains usable when Forma styles are unavailable.
- Explicit missing-evidence and qualified-source states.
- Four Forma static diagrams, each with a keyboard-scrollable viewport and a
  text relationship equivalent.
- axe WCAG 2.2 A/AA analysis on every route at 390 px: zero violations.
- Print media hides screen chrome and exposes Folio document, section, header,
  footer, figure, table, integrity, and finding primitives.

## Acceptance gates

| Gate | Status | Evidence |
|---|---|---|
| G01 Clean F# build | PASS | `npm run build`: zero warnings and zero errors. |
| G02 Corporate visual identity | PASS | Exact token/font assertions and four reviewed screenshots under `evidence/branding/screenshots/`; intentional differences in `docs/branding-contract.md`. |
| G03 Forma components | PASS | Pinned 0.9.0 marketing/theme assets, public component contracts, no-script fallback, focus/reflow/axe checks. |
| G04 Folio printable documents | PASS | Pinned 0.4.1 elements and stylesheet; four current PDFs; print primitive assertions and visual review. |
| G05 Dependency lockfile committed | PASS | `package-lock.json` is committed at `ce41cc4`; a fresh `npm ci` installed its exact dependency graph. |
| G06 Clean checkout builds reproducibly | PASS | Fresh shallow clone of pushed commit `ce41cc4`: `npm ci` followed by `npm test`; F# build had zero warnings/errors and all 22 tests passed. |
| G07 Responsive browser tests | PASS | Required six widths across all routes plus long-content stress. |
| G08 Accessibility tests | PASS | Keyboard, focus, landmarks, reduced motion, 200% reflow, text equivalents, and zero axe violations. |
| G09 Print tests | PASS | Four tagged Letter PDFs, 3–4 pages each, visually inspected from Poppler renders. |
| G10 GitHub Pages deployment | PASS | The Pages workflow and live checks verify every published route, internal links, Forma/Folio assets, console, mobile reflow, axe, and print media; the page-removal deployment is rechecked at delivery. |
| G11 Accessible diagrams in current scope | PASS | D01–D04 use Forma's static diagram contract and relationship lists without JavaScript; D05 left scope with the user-requested page removal. |
| G12 Rehearsal evidence source-linked | PASS | Exact revisions and primary report/verdict/ledger/clock links; observed/interpreted/hypothesized states remain separate. |
| G13 Requirements verification report | PASS | This report records every gate without converting unavailable evidence to success. |

## Remaining blockers

- Rehearsal 5 has no published run evidence at revision `52e9a74c…`; a
  cross-rehearsal performance claim is blocked by missing evidence.
- No website acceptance gate remains blocked. The unavailable Rehearsal 5
  evidence remains a documented source limitation, not a website defect.
