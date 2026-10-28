# Echelon Engineering System

Permanent, evidence-led documentation for Echelon Foundry's engineering
architecture, method, components, agent decision boundaries, and research.

The application is a static publisher written in F#. It uses Forma 0.9.0 for
the web, marketing, and diagram presentation contracts and Folio 0.4.1 for
print components and fragmentation rules. Essential content has no JavaScript
application runtime and does not require WebAssembly. Limen is intentionally
absent until a stateful browser requirement justifies it.

## Prerequisites

- .NET 10 SDK
- Node.js 24
- npm with lockfile support
- Chromium installed for Playwright (`npx playwright install chromium`)

## Reproducible build

From a clean checkout:

```sh
npm ci --ignore-scripts --no-audit --no-fund
npm run build
npm test
```

`npm run build` compiles `src/EngineeringSystem.Site` and runs the F#
publisher. The publisher writes `dist/`, installs the exact Forma and Folio
assets from `node_modules`, and fails on a missing route or asset, duplicate or
missing page heading, incorrect current navigation, or broken internal link.

Preview the generated site:

```sh
python3 -m http.server 4173 -d dist
```

Then open `http://127.0.0.1:4173/`.

## Verification

```sh
npm run test:browser   # responsive, keyboard, semantics, diagrams, axe, print media
npm run test:print     # four tagged Letter PDFs under output/pdf/
npm run test:branding  # requires the corporate reference build at /private/tmp/echelon-foundry-branding/dist
```

The browser suite covers all eight routes at 320, 390, 768, 851, 1000, and
1280 CSS pixels, 200% text scaling, reduced motion, keyboard focus, long-token
stress, missing styles, missing evidence, diagram text equivalents, and WCAG
2.2 A/AA axe checks. The print suite creates:

- [system architecture](output/pdf/echelon-system-architecture.pdf)
- [component inventory](output/pdf/echelon-component-inventory.pdf)
- [engineering lifecycle](output/pdf/echelon-engineering-lifecycle.pdf)
- [research summaries](output/pdf/echelon-research-summaries.pdf)

## Content and evidence

- [Brand contract](docs/branding-contract.md)
- [Versioned source inventory](docs/source-inventory.md)
- [Rehearsal evidence classification](docs/rehearsal-evidence.md)
- [Requirements verification](docs/requirements-verification.md)

The source inventory is authoritative for the revisions used by this
explanatory site. Upstream repositories continue to own their contracts and
evidence. Missing measurements remain unavailable rather than becoming zero.

## Deployment

`.github/workflows/pages.yml` runs exact dependency restore, F# build and
structural validation, Chromium browser/accessibility tests, PDF tests, artifact
upload, and GitHub Pages deployment. Pull requests run every verification step
without deploying; pushes to `main` deploy the verified artifact.

The configured production URL is
[engineering.echelonfoundry.com](https://engineering.echelonfoundry.com/).
