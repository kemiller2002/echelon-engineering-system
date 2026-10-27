# Source inventory

Reviewed 2026-10-27. Commit identifiers are the exact revisions used to write
the architecture and evidence pages. A repository relationship is not implied
merely because two systems appear in this inventory.

| Authority | Revision | Use in this website |
|---|---|---|
| [Echelon Foundry](https://github.com/kemiller2002/echelon-foundry/tree/8c6b45358fb1bcae4f612534637077d35dd72762) | `8c6b45358fb1bcae4f612534637077d35dd72762` | Corporate typography, palette, page shell, spacing, header, footer, cards, breakpoints, and accessibility conventions. |
| [Forma 0.9.0](https://github.com/kemiller2002/forma/tree/18e6ff7b15e54862521a101ac2589da0c597fc98) | tag `v0.9.0`, commit `18e6ff7b15e54862521a101ac2589da0c597fc98` | Installed web, marketing-theme, and accessible static-diagram contracts. |
| [Folio 0.4.1](https://github.com/kemiller2002/folio/tree/a94cea73dd4b4c5409fb0d4399ba65f7dbc51f5e) | tag `v0.4.1`, commit `a94cea73dd4b4c5409fb0d4399ba65f7dbc51f5e` | Installed light-DOM print components and Letter print stylesheet. |
| [Praxis](https://github.com/kemiller2002/praxis/tree/33d925027c4b286758d1773551bdf2c6b9fe65ec) | `33d925027c4b286758d1773551bdf2c6b9fe65ec` | Repository work protocol, durable research records, telemetry, provenance, validation, and continuity. |
| [Ordo](https://github.com/kemiller2002/ordo/tree/2df928ae4618eb075d226ff307843827486ffe39) | `2df928ae4618eb075d226ff307843827486ffe39` | State-directed engineering method and the required/approved/implied/unresolved/unsupported decision classification. |
| [Dokimos](https://github.com/kemiller2002/dokimos/tree/bf6a62bb6246177c30cc64af35921a300b7ddf9a) | `bf6a62bb6246177c30cc64af35921a300b7ddf9a` | Code-quality measurement and longitudinal evidence; its README explicitly leaves process telemetry to Praxis and application state legality to Ordo. |
| [Limen](https://github.com/kemiller2002/limen/tree/ddfc1e877cff1bbcb585952289f8eeae3062addd) | `ddfc1e877cff1bbcb585952289f8eeae3062addd` | Optional browser-capability boundary. It keeps the browser kernel separate from application authority and carries serializable data between them. |
| [Visual Engineering](https://github.com/kemiller2002/visual-engineering/tree/1bda5ff24493d75af02e02df4ca11fe1a417fc5c) | `1bda5ff24493d75af02e02df4ca11fe1a417fc5c` | Responsive, accessibility, motion, perceptual-robustness, and evidence conventions used by the test suite. |
| [Causa Data](https://github.com/kemiller2002/causa-data/tree/22260f429501d0d6963fc33302ed1e64d742a374) | `22260f429501d0d6963fc33302ed1e64d742a374` | Canonical Causa datastore, frozen storage contract, negative fixtures, and three-case historical demonstration catalog. Its `engine.lock.json` pins the authoritative F# engine. |
| [Indy Init](https://github.com/kemiller2002/Indy-init/tree/ad87474d92b42ebaff3a980525531da51e79ad6e) | `ad87474d92b42ebaff3a980525531da51e79ad6e` | Competition requirements, compiled requirement package, acceptance predicate, gate model, and published rehearsal index. |
| [Rehearsal 4](https://github.com/kemiller2002/indy-init-rehearsal-004/tree/b6947b8f69ee16e9e8e10f706b15bd1c4d22f9b1) | `b6947b8f69ee16e9e8e10f706b15bd1c4d22f9b1` | Final report, computed verdict, content-addressed evidence, acceptance ledger, reviews, clock, and implementation. |
| [Rehearsal 5](https://github.com/kemiller2002/indy-init-rehearsal-005/tree/52e9a74c870804eb72936439fbf79c651ef348c1) | `52e9a74c870804eb72936439fbf79c651ef348c1` | Current publication state only. This revision contains a one-line README and no run report or measurements. |

## Compatibility decisions

- The corporate site still declares Forma 0.2.0 and Folio 0.3.0, while this
  website uses the current approved releases Forma 0.9.0 and Folio 0.4.1.
  Visual parity is checked against the corporate reference rather than inferred
  from package names.
- Forma's `dist/marketing/forma-all.css` and Echelon marketing theme are copied
  by the F# publisher. Diagram output uses Forma's documented static 2.1.0
  contract and always includes a text relationship list.
- Folio's actual custom elements are passive light-DOM wrappers. This website
  uses those elements together with Folio's print stylesheet; importing a
  stylesheet alone is not treated as component integration.
- Limen is not installed. Static navigation, diagrams, and evidence do not need
  application state or browser capabilities. That decision can change only for
  a concrete interactive requirement.

## Unresolved source boundary

There is no public `kemiller2002/causa` repository at review time. The website
therefore describes Causa from the canonical datastore, its pinned engine, the
Indy Init requirements, and Rehearsal 4. It does not invent a standalone package
or integration contract.
