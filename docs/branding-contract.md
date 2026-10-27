# Echelon Foundry branding contract

Status: verified source mapping; visual screenshots generated during browser verification.

## Authority

- Corporate repository: `kemiller2002/echelon-foundry`
- Inspected revision: `8c6b45358fb1bcae4f612534637077d35dd72762`
- Primary source files: `assets/css/style.css`, `src/templates/layout.html`, `src/templates/header.html`, and `src/templates/footer.html`
- Forma dependency declared by the corporate site: `@echelon-foundry/design-system@0.2.0`
- Corporate consumption finding: the deployed source does not load Forma's CSS exports; it implements the identity in `assets/css/style.css` and uses a vendored Forma conformance harness for tests.

## Identity mapped to the engineering site

| Role | Corporate value | Engineering implementation |
| --- | --- | --- |
| Display face | Newsreader 500/650 | Forma Echelon marketing theme, Newsreader 500 |
| Body face | Manrope 400-700 | Forma Echelon marketing theme |
| Monospace face | IBM Plex Mono 400/500 | Forma Echelon marketing theme |
| Parchment | `#f2efe7` | `--ef-color-surface-primary` |
| Charcoal | `#202421` | `--ef-color-text-heading` and inverse surface |
| Carbon | `#171a18` | `--ef-color-text-primary` |
| Oxide bronze | `#905831` | `--ef-color-accent-primary` |
| Verdigris | `#47756b` | `--ef-color-accent-secondary` |
| Stone | `#e3e0d7` | secondary surface and subtle border |
| Max shell width | 1440px | Forma `--ef-layout-width-max: 90rem` |
| Gutters | `clamp(1rem, 4vw, 4.5rem)` | Forma Echelon marketing theme |
| Section rhythm | `clamp(5rem, 10vw, 10rem)` | Forma Echelon marketing section contract |
| Focus | parchment gap, carbon 3px ring + halo | Forma focus contract with the same tokens |

## Reused composition

The engineering site uses Forma's current MarketingShell, site header, navigation, hero, section heading, cards, facts, entry index, steps, CTA, callout, badges, documentation, timeline, table, and diagram contracts. This preserves the corporate square `EF` mark, sticky bordered header, border-driven cards, drafting-grid backdrop, inverse footer, responsive recomposition, and reduced-motion/forced-color behavior without copying the corporate stylesheet.

## Intentional differences

- The engineering site has a denser documentation navigation because it exposes nine permanent information areas. Forma owns wrapping and target sizing at narrow widths.
- The engineering site loads Forma `0.9.0`, the latest published release at implementation time. The corporate site declares `0.2.0`; aligning backward would remove the marketing, diagram, accessibility, and conformance contracts required by this site.
- Printable pages add Folio `0.4.1` semantics and print-only headers/footers. The corporate marketing site does not expose that documentation surface.
- The corporate repository's Visual Engineering install was incomplete on the inspected revision. A read-only `visual-engineering init` reconstruction using its pinned `1.0.0` package was verified before review; this did not change the authority revision.

## Screenshot evidence

- `evidence/branding/screenshots/corporate-390.png`
- `evidence/branding/screenshots/engineering-390.png`
- `evidence/branding/screenshots/corporate-1280.png`
- `evidence/branding/screenshots/engineering-1280.png`

The screenshots are equivalent viewport captures, not pixel-diff assertions: the content and information density intentionally differ. Automated checks assert the exact identity tokens and font contracts.
