# Engineering system website verification

Last updated: 2026-10-27. Gate state is evidence-driven; a commit by itself is
not acceptance. `PENDING` below is represented as `NOT RUN` until the named
external or clean-checkout check completes.

## Baseline and implementation

- Repository: `kemiller2002/echelon-engineering-system`
- Branch: `codex/complete-engineering-system-site`
- Starting revision: `99e4fcbd489fbd98ab897ae083034e7523e1456d`
- Baseline defect: the existing F# project failed with FS0010 in `Program.fs`.
- Dependency defects: Forma 0.9.1 did not exist in the package registry; Folio
  0.4.2 did not exist as a published release. The implementation pins the
  verified releases Forma 0.9.0 and Folio 0.4.1.
- Deployment configuration: GitHub Pages is configured for Actions at
  `https://engineering.echelonfoundry.com/`.

## Commands executed

| Check | Result |
|---|---|
| `npm run build` | PASS — .NET 10 build, zero warnings/errors; F# publisher generated and structurally validated 9 routes and 5 required assets. |
| `npm test` | PASS — 22 Playwright checks. |
| `npm run test:branding` | PASS — 5 checks against the built corporate reference at 390 and 1280 px. |
| `npm run test:print` | PASS — 6 checks; five tagged US Letter PDFs generated. |
| Poppler `pdfinfo`, `pdftoppm`, and visual page inspection | PASS — 19 rendered pages reviewed after correcting a print-width defect; tables, diagrams, headings, page breaks, and grayscale/background-disabled output are readable. |
| `git diff --check` | PASS. |
| Fresh-checkout `npm ci`, build, and tests | NOT RUN — scheduled after the first pushed checkpoint. |
| Production URL inspection | NOT RUN — scheduled after merge and Pages deployment. |

## Browser and accessibility coverage

- All nine routes at widths 320, 390, 768, 851, 1000, and 1280 px.
- No page-level horizontal overflow.
- 200% text scaling, reduced motion, keyboard skip navigation, visible focus,
  semantic landmarks, exactly one H1, and current-page navigation.
- Adversarial unbroken component names and URLs.
- Content remains usable when Forma styles are unavailable.
- Explicit missing-evidence and qualified-source states.
- Five Forma static diagrams, each with a keyboard-scrollable viewport and a
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
| G04 Folio printable documents | PASS | Pinned 0.4.1 elements and stylesheet; five actual PDFs; print primitive assertions and visual review. |
| G05 Dependency lockfile committed | NOT RUN | `package-lock.json` exists; gate changes only after the checkpoint commit. |
| G06 Clean checkout builds reproducibly | NOT RUN | Fresh-checkout check pending. |
| G07 Responsive browser tests | PASS | Required six widths across all routes plus long-content stress. |
| G08 Accessibility tests | PASS | Keyboard, focus, landmarks, reduced motion, 200% reflow, text equivalents, and zero axe violations. |
| G09 Print tests | PASS | Five tagged Letter PDFs, 3–4 pages each, visually inspected from Poppler renders. |
| G10 GitHub Pages deployment | NOT RUN | Repository setting is ready; workflow and deployed SHA not yet verified. |
| G11 Five accessible diagrams | PASS | D01–D05 use Forma's static diagram contract and relationship lists without JavaScript. |
| G12 Rehearsal evidence source-linked | PASS | Exact revisions and primary report/verdict/ledger/clock links; observed/interpreted/hypothesized states remain separate. |
| G13 Requirements verification report | PASS | This report records every gate without converting unavailable evidence to success. |

## Remaining blockers

- Rehearsal 5 has no published run evidence at revision `52e9a74c…`; a
  cross-rehearsal performance claim is blocked by missing evidence.
- G06 and G10 remain NOT RUN until the pushed branch is tested from a fresh
  checkout and the deployed `main` revision is inspected.
