namespace Echelon.EngineeringSystem

open System
open System.IO
open System.Net
open System.Text.RegularExpressions

type EvidenceStatus =
    | Verified
    | Documented
    | Experimental
    | Planned
    | Unknown

type Page =
    { Slug: string
      NavLabel: string
      Eyebrow: string
      Title: string
      Summary: string
      Content: string }

module Site =
    let private encode (value: string) = WebUtility.HtmlEncode(value)

    let private statusText = function
        | Verified -> "Verified"
        | Documented -> "Documented contract"
        | Experimental -> "Experimental"
        | Planned -> "Planned"
        | Unknown -> "Unknown"

    let private statusVariant = function
        | Verified -> "success"
        | Documented -> "info"
        | Experimental -> "warning"
        | Planned -> "warning"
        | Unknown -> "danger"

    let private statusBadge status =
        $"<span class=\"ef-badge\" data-ef-status=\"{statusVariant status}\">{statusText status}</span>"

    let private card eyebrow title body footer href =
        let heading =
            match href with
            | Some destination -> $"<h3 class=\"ef-card__title\"><a class=\"ef-card__link\" href=\"{destination}\">{encode title}</a></h3>"
            | None -> $"<h3 class=\"ef-card__title\">{encode title}</h3>"

        let cardFooter =
            footer
            |> Option.map (fun value -> $"<p class=\"ef-card__footer\">{value}</p>")
            |> Option.defaultValue ""

        $"<li><article class=\"ef-card\"><p class=\"ef-eyebrow\">{encode eyebrow}</p>{heading}<p>{body}</p>{cardFooter}</article></li>"

    let private cards label columns items =
        let content = String.concat "" items
        $"<ul class=\"ef-card-grid\" data-ef-columns=\"{columns}\" aria-label=\"{encode label}\">{content}</ul>"

    let private section id eyebrow title intro content =
        $"""<ef-print-section>
<section class="ef-section" id="{id}" aria-labelledby="{id}-title">
  <header class="ef-section-heading">
    <div class="ef-section-heading__text">
      <p class="ef-eyebrow">{encode eyebrow}</p>
      <h2 id="{id}-title">{encode title}</h2>
      <p>{intro}</p>
    </div>
  </header>
  {content}
</section>
</ef-print-section>"""

    let private systemDiagram =
        """<ef-print-figure><figure class="ef-diagram" id="d01" aria-labelledby="d01-title">
<figcaption id="d01-title">D01 — Echelon system architecture</figcaption>
<p>Solid lines are relationships stated by the cited sources. The dashed line is an optional browser capability that this website does not use. Position does not imply ownership.</p>
<div class="ef-diagram__viewport" tabindex="0" role="group" aria-label="Echelon system architecture canvas, scroll to see all systems">
<div class="ef-diagram__canvas" style="--ef-diagram-canvas-w: 660px; --ef-diagram-canvas-h: 570px;">
<svg class="ef-diagram__wires" viewBox="0 0 660 570" aria-hidden="true" focusable="false"><defs><marker id="d01-arrow" class="ef-diagram-marker" viewBox="0 0 10 10" refX="9" refY="5" markerWidth="8" markerHeight="8" markerUnits="userSpaceOnUse" orient="auto"><path d="M0 0 L10 5 L0 10 z"/></marker></defs>
<path class="ef-diagram-connector" d="M220 73 H190 V165 H198" marker-end="url(#d01-arrow)"/><path class="ef-diagram-connector" d="M440 73 H470 V165 H462" marker-end="url(#d01-arrow)"/>
<path class="ef-diagram-connector" d="M330 125 V157" marker-end="url(#d01-arrow)"/><path class="ef-diagram-connector" d="M110 260 V300" marker-end="url(#d01-arrow)"/>
<path class="ef-diagram-connector" d="M190 350 H228" marker-end="url(#d01-arrow)"/><path class="ef-diagram-connector" d="M430 350 H468" marker-end="url(#d01-arrow)"/>
<path class="ef-diagram-connector" data-ef-line="dashed" d="M110 402 V442 H228" marker-end="url(#d01-arrow)"/><path class="ef-diagram-connector" d="M550 260 V300" marker-end="url(#d01-arrow)"/></svg>
<article class="ef-diagram-node" aria-labelledby="d01-praxis" style="--ef-diagram-x: 220px; --ef-diagram-y: 20px; --ef-diagram-w: 220px;"><p class="ef-diagram-node__kind">Repository authority</p><h3 class="ef-diagram-node__label" id="d01-praxis">Praxis</h3><p class="ef-diagram-node__description">Work, provenance, evidence, validation, continuity</p></article>
<article class="ef-diagram-node" aria-labelledby="d01-ordo" style="--ef-diagram-x: 10px; --ef-diagram-y: 160px; --ef-diagram-w: 180px;"><p class="ef-diagram-node__kind">Method authority</p><h3 class="ef-diagram-node__label" id="d01-ordo">Ordo</h3><p class="ef-diagram-node__description">Choices, state, legal transitions</p></article>
<article class="ef-diagram-node" aria-labelledby="d01-dokimos" style="--ef-diagram-x: 240px; --ef-diagram-y: 160px; --ef-diagram-w: 180px;"><p class="ef-diagram-node__kind">Quality evidence</p><h3 class="ef-diagram-node__label" id="d01-dokimos">Dokimos</h3><p class="ef-diagram-node__description">Measurements, trends, regressions</p></article>
<article class="ef-diagram-node" aria-labelledby="d01-ve" style="--ef-diagram-x: 470px; --ef-diagram-y: 160px; --ef-diagram-w: 180px;"><p class="ef-diagram-node__kind">Experience evidence</p><h3 class="ef-diagram-node__label" id="d01-ve">Visual Engineering</h3><p class="ef-diagram-node__description">Layout, access, robustness</p></article>
<article class="ef-diagram-node" aria-labelledby="d01-causa" style="--ef-diagram-x: 10px; --ef-diagram-y: 300px; --ef-diagram-w: 180px;"><p class="ef-diagram-node__kind">Domain application</p><h3 class="ef-diagram-node__label" id="d01-causa">Causa</h3><p class="ef-diagram-node__description">Root-cause investigations and evidence</p></article>
<article class="ef-diagram-node" aria-labelledby="d01-forma" style="--ef-diagram-x: 240px; --ef-diagram-y: 300px; --ef-diagram-w: 180px;"><p class="ef-diagram-node__kind">Screen presentation</p><h3 class="ef-diagram-node__label" id="d01-forma">Forma</h3><p class="ef-diagram-node__description">Semantic web and diagrams</p></article>
<article class="ef-diagram-node" aria-labelledby="d01-folio" style="--ef-diagram-x: 470px; --ef-diagram-y: 300px; --ef-diagram-w: 180px;"><p class="ef-diagram-node__kind">Print presentation</p><h3 class="ef-diagram-node__label" id="d01-folio">Folio</h3><p class="ef-diagram-node__description">Documents and reports</p></article>
<article class="ef-diagram-node" data-ef-status="pending" aria-labelledby="d01-limen" style="--ef-diagram-x: 240px; --ef-diagram-y: 430px; --ef-diagram-w: 180px;"><p class="ef-diagram-node__kind">Optional boundary</p><h3 class="ef-diagram-node__label" id="d01-limen">Limen</h3><p class="ef-diagram-node__description">Browser capability, no domain authority</p></article>
</div></div>
<ol class="ef-diagram__relations" aria-label="D01 relationships"><li>Ordo emits versioned decision and transition observations that Praxis can preserve without taking over Ordo's semantic authority.</li><li>Dokimos owns code-quality evidence while Praxis owns process telemetry and Ordo owns application-state legality.</li><li>Visual Engineering supplies conformance criteria for Forma screen output and Folio print output.</li><li>Rehearsal 4 applied Praxis governance to the Causa implementation.</li><li>Causa's rehearsal UI used Forma for screen presentation and Folio for printable reports.</li><li>Causa used Limen as a browser-capability boundary; this is a documented consumer relationship, not a requirement for every site.</li></ol>
</figure></ef-print-figure>"""

    let private lifecycleDiagram =
        """<ef-print-figure><figure class="ef-diagram" id="d02" aria-labelledby="d02-title"><figcaption id="d02-title">D02 — Requirements to verified implementation</figcaption><p>The main path is solid. Failed verification returns work to correction; an invalid or unsupported choice stops before implementation.</p><div class="ef-diagram__viewport" tabindex="0" role="group" aria-label="Requirements lifecycle canvas, scroll to see every step"><div class="ef-diagram__canvas" style="--ef-diagram-canvas-w: 660px; --ef-diagram-canvas-h: 390px;"><svg class="ef-diagram__wires" viewBox="0 0 660 390" aria-hidden="true" focusable="false"><defs><marker id="d02-arrow" class="ef-diagram-marker" viewBox="0 0 10 10" refX="9" refY="5" markerWidth="8" markerHeight="8" markerUnits="userSpaceOnUse" orient="auto"><path d="M0 0 L10 5 L0 10 z"/></marker></defs><path class="ef-diagram-connector" d="M150 72 H180" marker-end="url(#d02-arrow)"/><path class="ef-diagram-connector" d="M310 72 H340" marker-end="url(#d02-arrow)"/><path class="ef-diagram-connector" d="M470 72 H500" marker-end="url(#d02-arrow)"/><path class="ef-diagram-connector" d="M565 124 V214" marker-end="url(#d02-arrow)"/><path class="ef-diagram-connector" d="M500 270 H470" marker-end="url(#d02-arrow)"/><path class="ef-diagram-connector" d="M340 270 H310" marker-end="url(#d02-arrow)"/><path class="ef-diagram-connector" d="M180 270 H150" marker-end="url(#d02-arrow)"/><path class="ef-diagram-connector" data-ef-line="dashed" d="M405 214 V160 H245 V126" marker-end="url(#d02-arrow)"/></svg>
<article class="ef-diagram-node" data-ef-shape="pill" aria-labelledby="d02-req" style="--ef-diagram-x: 20px; --ef-diagram-y: 20px; --ef-diagram-w: 130px;"><p class="ef-diagram-node__kind">Start</p><h3 class="ef-diagram-node__label" id="d02-req">Requirements</h3></article><article class="ef-diagram-node" aria-labelledby="d02-class" style="--ef-diagram-x: 180px; --ef-diagram-y: 20px; --ef-diagram-w: 130px;"><p class="ef-diagram-node__kind">Boundary</p><h3 class="ef-diagram-node__label" id="d02-class">Classify choices</h3></article><article class="ef-diagram-node" aria-labelledby="d02-plan" style="--ef-diagram-x: 340px; --ef-diagram-y: 20px; --ef-diagram-w: 130px;"><p class="ef-diagram-node__kind">Plan</p><h3 class="ef-diagram-node__label" id="d02-plan">Bounded plan</h3></article><article class="ef-diagram-node" aria-labelledby="d02-impl" style="--ef-diagram-x: 500px; --ef-diagram-y: 20px; --ef-diagram-w: 130px;"><p class="ef-diagram-node__kind">Execute</p><h3 class="ef-diagram-node__label" id="d02-impl">Implementation</h3></article><article class="ef-diagram-node" aria-labelledby="d02-verify" style="--ef-diagram-x: 500px; --ef-diagram-y: 220px; --ef-diagram-w: 130px;"><p class="ef-diagram-node__kind">Gate</p><h3 class="ef-diagram-node__label" id="d02-verify">Verification</h3></article><article class="ef-diagram-node" aria-labelledby="d02-evidence" style="--ef-diagram-x: 340px; --ef-diagram-y: 220px; --ef-diagram-w: 130px;"><p class="ef-diagram-node__kind">Record</p><h3 class="ef-diagram-node__label" id="d02-evidence">Evidence</h3></article><article class="ef-diagram-node" aria-labelledby="d02-accept" style="--ef-diagram-x: 180px; --ef-diagram-y: 220px; --ef-diagram-w: 130px;"><p class="ef-diagram-node__kind">Decision</p><h3 class="ef-diagram-node__label" id="d02-accept">Accept or block</h3></article><article class="ef-diagram-node" data-ef-shape="pill" aria-labelledby="d02-handoff" style="--ef-diagram-x: 20px; --ef-diagram-y: 220px; --ef-diagram-w: 130px;"><p class="ef-diagram-node__kind">Durable state</p><h3 class="ef-diagram-node__label" id="d02-handoff">Release or handoff</h3></article></div></div><ol class="ef-diagram__relations" aria-label="D02 relationships"><li>Requirements lead to decision classification, then a bounded plan and implementation.</li><li>Implementation must pass proportionate verification before evidence can support acceptance.</li><li>Failed verification returns to classification and planning for correction.</li><li>Acceptance produces a releasable result; a blocker produces a durable handoff instead of a false success.</li></ol></figure></ef-print-figure>"""

    let private agentDiagram =
        """<ef-print-figure><figure class="ef-diagram" id="d03" aria-labelledby="d03-title"><figcaption id="d03-title">D03 — Agent decision boundaries</figcaption><p>Classification reports the authority already present in requirements and approved records. It does not manufacture authority.</p><div class="ef-diagram__viewport" tabindex="0" role="group" aria-label="Agent decision boundaries canvas, scroll to see every category"><div class="ef-diagram__canvas" style="--ef-diagram-canvas-w: 660px; --ef-diagram-canvas-h: 420px;"><svg class="ef-diagram__wires" viewBox="0 0 660 420" aria-hidden="true" focusable="false"><defs><marker id="d03-arrow" class="ef-diagram-marker" viewBox="0 0 10 10" refX="9" refY="5" markerWidth="8" markerHeight="8" markerUnits="userSpaceOnUse" orient="auto"><path d="M0 0 L10 5 L0 10 z"/></marker></defs><path class="ef-diagram-connector" d="M330 105 V135 H88 V165" marker-end="url(#d03-arrow)"/><path class="ef-diagram-connector" d="M330 105 V165" marker-end="url(#d03-arrow)"/><path class="ef-diagram-connector" d="M330 135 H572 V165" marker-end="url(#d03-arrow)"/><path class="ef-diagram-connector" d="M88 260 V320 H238" marker-end="url(#d03-arrow)"/><path class="ef-diagram-connector" d="M330 260 V320" marker-end="url(#d03-arrow)"/><path class="ef-diagram-connector" data-ef-line="dashed" d="M572 260 V320 H422" marker-end="url(#d03-arrow)"/></svg><article class="ef-diagram-node" data-ef-shape="pill" aria-labelledby="d03-human" style="--ef-diagram-x: 230px; --ef-diagram-y: 20px; --ef-diagram-w: 200px;"><p class="ef-diagram-node__kind">Human authority</p><h3 class="ef-diagram-node__label" id="d03-human">Goal, policy, approvals</h3></article><article class="ef-diagram-node" aria-labelledby="d03-fixed" style="--ef-diagram-x: 10px; --ef-diagram-y: 165px; --ef-diagram-w: 156px;"><p class="ef-diagram-node__kind">Required / approved</p><h3 class="ef-diagram-node__label" id="d03-fixed">Predetermined</h3><p class="ef-diagram-node__description">Implement as specified</p></article><article class="ef-diagram-node" aria-labelledby="d03-delegated" style="--ef-diagram-x: 252px; --ef-diagram-y: 165px; --ef-diagram-w: 156px;"><p class="ef-diagram-node__kind">Bounded discretion</p><h3 class="ef-diagram-node__label" id="d03-delegated">Delegated</h3><p class="ef-diagram-node__description">Choose reversible details</p></article><article class="ef-diagram-node" data-ef-status="blocked" aria-labelledby="d03-stop" style="--ef-diagram-x: 494px; --ef-diagram-y: 165px; --ef-diagram-w: 156px;"><p class="ef-diagram-node__kind">Unresolved / unsupported</p><h3 class="ef-diagram-node__label" id="d03-stop">Stop and escalate</h3><p class="ef-diagram-node__description">Do not silently decide</p></article><article class="ef-diagram-node" data-ef-shape="pill" aria-labelledby="d03-gate" style="--ef-diagram-x: 230px; --ef-diagram-y: 310px; --ef-diagram-w: 200px;"><p class="ef-diagram-node__kind">Verification gate</p><h3 class="ef-diagram-node__label" id="d03-gate">Evidence supports transition?</h3></article></div></div><ol class="ef-diagram__relations" aria-label="D03 relationships"><li>Human requirements and approved decisions fix predetermined choices.</li><li>Agents may decide reversible implementation details only inside the delegated boundary.</li><li>An unresolved or unsupported choice cannot be emitted as completed construction; it stops for explicit resolution.</li><li>Predetermined and delegated work still pass through verification before completion.</li><li>A material scope change or consequential external action returns to human approval.</li></ol></figure></ef-print-figure>"""

    let private componentDiagram =
        """<ef-print-figure><figure class="ef-diagram" id="d04" aria-labelledby="d04-title"><figcaption id="d04-title">D04 — Website component relationships</figcaption><p>Solid lines are required by this repository's build. The dashed Limen path is an optional capability and is deliberately absent.</p><div class="ef-diagram__viewport" tabindex="0" role="group" aria-label="Website dependency canvas, scroll to see every component"><div class="ef-diagram__canvas" style="--ef-diagram-canvas-w: 660px; --ef-diagram-canvas-h: 450px;"><svg class="ef-diagram__wires" viewBox="0 0 660 450" aria-hidden="true" focusable="false"><defs><marker id="d04-arrow" class="ef-diagram-marker" viewBox="0 0 10 10" refX="9" refY="5" markerWidth="8" markerHeight="8" markerUnits="userSpaceOnUse" orient="auto"><path d="M0 0 L10 5 L0 10 z"/></marker></defs><path class="ef-diagram-connector" d="M160 78 H200" marker-end="url(#d04-arrow)"/><path class="ef-diagram-connector" d="M330 78 H370" marker-end="url(#d04-arrow)"/><path class="ef-diagram-connector" d="M265 125 V155 H565 V125" marker-end="url(#d04-arrow)"/><path class="ef-diagram-connector" d="M435 125 V175 H565 V210" marker-end="url(#d04-arrow)"/><path class="ef-diagram-connector" d="M570 125 V210" marker-end="url(#d04-arrow)"/><path class="ef-diagram-connector" d="M500 265 H440 V355 H420" marker-end="url(#d04-arrow)"/><path class="ef-diagram-connector" d="M280 355 H230" marker-end="url(#d04-arrow)"/><path class="ef-diagram-connector" data-ef-line="dashed" d="M160 220 H500" marker-end="url(#d04-arrow)"/></svg><article class="ef-diagram-node" aria-labelledby="d04-content" style="--ef-diagram-x: 20px; --ef-diagram-y: 25px; --ef-diagram-w: 140px;"><p class="ef-diagram-node__kind">Source</p><h3 class="ef-diagram-node__label" id="d04-content">F# models and routes</h3></article><article class="ef-diagram-node" aria-labelledby="d04-publish" style="--ef-diagram-x: 200px; --ef-diagram-y: 25px; --ef-diagram-w: 130px;"><p class="ef-diagram-node__kind">Required</p><h3 class="ef-diagram-node__label" id="d04-publish">F# publisher</h3></article><article class="ef-diagram-node" aria-labelledby="d04-forma" style="--ef-diagram-x: 370px; --ef-diagram-y: 25px; --ef-diagram-w: 130px;"><p class="ef-diagram-node__kind">Required</p><h3 class="ef-diagram-node__label" id="d04-forma">Forma 0.9.0</h3></article><article class="ef-diagram-node" aria-labelledby="d04-folio" style="--ef-diagram-x: 500px; --ef-diagram-y: 25px; --ef-diagram-w: 140px;"><p class="ef-diagram-node__kind">Required</p><h3 class="ef-diagram-node__label" id="d04-folio">Folio 0.4.1</h3></article><article class="ef-diagram-node" data-ef-status="skipped" aria-labelledby="d04-limen" style="--ef-diagram-x: 20px; --ef-diagram-y: 175px; --ef-diagram-w: 140px;"><p class="ef-diagram-node__kind">Optional / unused</p><h3 class="ef-diagram-node__label" id="d04-limen">Limen</h3></article><article class="ef-diagram-node" aria-labelledby="d04-html" style="--ef-diagram-x: 500px; --ef-diagram-y: 210px; --ef-diagram-w: 140px;"><p class="ef-diagram-node__kind">Artifact</p><h3 class="ef-diagram-node__label" id="d04-html">Static HTML site</h3></article><article class="ef-diagram-node" aria-labelledby="d04-tests" style="--ef-diagram-x: 280px; --ef-diagram-y: 310px; --ef-diagram-w: 140px;"><p class="ef-diagram-node__kind">Gate</p><h3 class="ef-diagram-node__label" id="d04-tests">Browser, axe, print</h3></article><article class="ef-diagram-node" data-ef-shape="pill" aria-labelledby="d04-pages" style="--ef-diagram-x: 45px; --ef-diagram-y: 310px; --ef-diagram-w: 185px;"><p class="ef-diagram-node__kind">Delivery</p><h3 class="ef-diagram-node__label" id="d04-pages">GitHub Pages + PDFs</h3></article></div></div><ol class="ef-diagram__relations" aria-label="D04 relationships"><li>F# owns route definitions, content models, HTML generation, asset copying, and structural validation.</li><li>The F# publisher consumes pinned Forma and Folio release assets.</li><li>Forma styles semantic screen and diagram markup; Folio wraps printable content and supplies print rules.</li><li>The static result passes browser, axe, and PDF tests before publication to GitHub Pages.</li><li>Limen is not a runtime dependency because the website has no justified stateful browser interaction.</li></ol></figure></ef-print-figure>"""

    let private homeContent =
        let pathways =
            cards "Ways to explore the engineering system" "3"
                [ card "Architecture" "See the system of systems" "Inspect bounded responsibilities, source authority, and integration status." None (Some "system/")
                  card "Lifecycle" "Follow work from requirement to evidence" "See legal transitions, verification gates, and recovery paths." None (Some "process/")
                  card "Authority" "Understand agent decision boundaries" "Separate human policy, predetermined constraints, delegated choices, and prohibited transitions." None (Some "agents/") ]

        let foundation =
            cards "Delivery foundations" "4"
                [ card "Application" "F# static publishing" "Routes, content models, asset installation, HTML generation, and structural validation execute in F#." (Some(statusBadge Verified)) None
                  card "Presentation" "Forma 0.9.0" "The pinned Echelon marketing surface supplies typography, navigation, cards, layout, focus, and diagram contracts." (Some(statusBadge Verified)) None
                  card "Print" "Folio 0.4.1" "Printable documents use Folio light-DOM elements and its Letter-page fragmentation rules." (Some(statusBadge Verified)) None
                  card "Browser runtime" "No application runtime" "Essential documentation, diagrams, navigation, and evidence remain available without WebAssembly or JavaScript." (Some(statusBadge Verified)) None ]

        String.concat "\n"
            [ section "explore" "Two reading paths" "Orient quickly or inspect the evidence" "The site supports a short guided explanation and a deeper technical reference without hiding unresolved conditions." pathways
              section "foundation" "P0 foundation" "A static site designed to survive scrutiny" "The first release favors durable semantic HTML and build-time verification over unnecessary runtime behavior." foundation
              """<section class="ef-section" aria-labelledby="tour-cta-title"><div class="ef-cta" data-ef-tone="inverse"><div class="ef-cta__text"><p class="ef-eyebrow">Five-minute tour</p><h2 id="tour-cta-title">From intent to a verified result</h2><p>Walk through requirements, constraints, execution, evidence, and acceptance in source order.</p></div><div class="ef-actions"><a class="ef-button" data-ef-variant="primary" href="tour/">Start the guided tour</a><a class="ef-button" href="reference/">Review sources and limitations</a></div></div></section>""" ]

    let private systemContent =
        let components =
            cards "Core component responsibilities" "3"
                [ card "Governed execution" "Praxis" "Repository work, provenance, validation, evidence, and durable continuity." (Some(statusBadge Documented)) (Some "https://github.com/kemiller2002/praxis/tree/33d925027c4b286758d1773551bdf2c6b9fe65ec")
                  card "Method" "Ordo" "State-directed engineering concepts, legal transitions, and explicit decision boundaries." (Some(statusBadge Documented)) (Some "https://github.com/kemiller2002/ordo/tree/2df928ae4618eb075d226ff307843827486ffe39")
                  card "Presentation" "Forma" "Zero-runtime semantic HTML, CSS, accessibility, marketing, and diagram presentation contracts." (Some(statusBadge Verified)) (Some "https://github.com/kemiller2002/forma")
                  card "Print" "Folio" "CSS-first printable documents and passive light-DOM print components." (Some(statusBadge Verified)) (Some "https://github.com/kemiller2002/folio")
                  card "Browser boundary" "Limen" "Optional stateful browser and WebAssembly boundary; not required by this static site." (Some(statusBadge Documented)) (Some "https://github.com/kemiller2002/limen/tree/ddfc1e877cff1bbcb585952289f8eeae3062addd")
                  card "Experience evidence" "Visual Engineering" "Evidence-led layout, accessibility, interaction, and conformance criteria." (Some(statusBadge Documented)) (Some "https://github.com/kemiller2002/visual-engineering/tree/1bda5ff24493d75af02e02df4ca11fe1a417fc5c") ]

        String.concat "\n"
            [ section "architecture-map" "D01 · System architecture" "Responsibilities and evidenced relationships" "The map includes the systems named by the mission while keeping domain, process, quality, presentation, and browser authority separate." systemDiagram
              section "responsibilities" "Architecture atlas" "Bounded responsibilities before integrations" "Adjacency does not prove a dependency. Every relationship on this site is labeled by evidence status and linked to its source." components
              section "integration-rule" "Evidence rule" "A system map is not an integration claim" "Verified implementation, documented contract, experiment, proposal, and unknown are separate states. The diagram and component directory preserve those distinctions." """<aside class="ef-callout" data-ef-callout="decision" aria-labelledby="integration-decision"><p class="ef-callout__label" id="integration-decision">Decision</p><p>Static documentation uses Forma and Folio directly. Limen remains optional until a stateful browser feature has a demonstrated need.</p></aside>""" ]

    let private processContent =
        let steps =
            """<ol class="ef-steps" role="list" aria-label="Engineering lifecycle">
<li><strong>Define.</strong> State the requirement, constraints, source authority, and measurable acceptance criteria.</li>
<li><strong>Classify.</strong> Separate predetermined decisions, delegated choices, required approvals, and prohibited actions.</li>
<li><strong>Plan.</strong> Select the smallest process that preserves correctness, traceability, and continuity.</li>
<li><strong>Execute.</strong> Implement in bounded slices and preserve evidence at coherent recovery points.</li>
<li><strong>Verify.</strong> Run applicable build, structural, browser, accessibility, and print checks.</li>
<li><strong>Accept or recover.</strong> Record pass, fail, blocked, or not run; resolve failures or hand off exact blockers.</li>
</ol>"""

        let gates =
            cards "Lifecycle gates" "3"
                [ card "Before execution" "Authorization gate" "The proposed transition must be legal and inside the granted scope." (Some(statusBadge Documented)) None
                  card "During execution" "Evidence gate" "Important claims and completed work must have durable, inspectable support." (Some(statusBadge Documented)) None
                  card "Before completion" "Verification gate" "Completion requires the checks appropriate to the risk; unavailable checks stay visible." (Some(statusBadge Documented)) None ]

        String.concat "\n"
            [ section "lifecycle-map" "D02 · Engineering lifecycle" "The actual path includes rejection and recovery" "A requirement reaches acceptance only through classified choices, bounded execution, proportionate verification, and durable evidence." lifecycleDiagram
              section "lifecycle" "Requirements to verified implementation" "The actual work path includes rejection and recovery" "The lifecycle is not a success-only funnel. Invalid transitions stop before execution; failed checks return work to correction." steps
              section "gates" "Control points" "Verification changes what may happen next" "Human authority, machine-enforced rules, and evidence gates remain distinct." gates ]

    let private agentsContent =
        let boundaries =
            cards "Decision boundary categories" "4"
                [ card "Human authority" "Defined goal and policy" "Humans set the objective, approve material scope changes, and retain authority over consequential external actions." (Some(statusBadge Documented)) None
                  card "Predetermined" "Constraints and legal transitions" "Repository policy, acceptance criteria, safety constraints, and typed state transitions narrow the legal decision space." (Some(statusBadge Documented)) None
                  card "Delegated" "Reversible implementation choices" "Agents may choose bounded technical details when those choices do not materially change the authorized goal." (Some(statusBadge Documented)) None
                  card "Prohibited" "Invalid or unsupported action" "The agent must reject unsafe, destructive, unverified, or out-of-scope transitions and report the condition." (Some(statusBadge Documented)) None ]

        String.concat "\n"
            [ section "boundary-map" "D03 · Decision boundaries" "Authority is explicit, not inferred from capability" "Ordo's classification keeps required, approved, implied, unresolved, and unsupported choices mechanically distinct." agentDiagram
              section "boundaries" "Agent architecture" "Authority is explicit, not inferred from capability" "A capable agent is still constrained by requirements, repository policy, transition rules, evidence, and external authorization." boundaries
              section "example" "Illustrative failure" "An invalid completion is rejected before it becomes history" "This example is simulated; it demonstrates the boundary without claiming a recorded production event." """<ef-print-finding compact><dl><div><dt>Proposed action</dt><dd>Mark a deployment gate PASS after only a local build.</dd></div><div><dt>Why invalid</dt><dd>The acceptance gate requires verification of the deployed GitHub Pages site.</dd></div><div><dt>Required recovery</dt><dd>Keep the gate NOT RUN or BLOCKED, deploy through the approved workflow, then inspect the actual URL.</dd></div></dl></ef-print-finding>""" ]

    let private componentsContent =
        let catalog =
            """<div class="site-table-wrap" role="region" aria-label="Component inventory" tabindex="0"><ef-print-table><table><colgroup><col style="width: 14%"><col style="width: 57%"><col style="width: 18%"><col style="width: 11%"></colgroup><caption>Component directory - status is evidence state, not product maturity.</caption><thead><tr><th scope="col">Component</th><th scope="col">Responsibility</th><th scope="col">Status</th><th scope="col">Authority</th></tr></thead><tbody>
<tr><th scope="row">Praxis</th><td>Repository operating system, execution evidence, and continuity.</td><td>Documented</td><td><a href="https://github.com/kemiller2002/praxis/tree/33d925027c4b286758d1773551bdf2c6b9fe65ec">Source</a></td></tr>
<tr><th scope="row">Ordo</th><td>State-directed engineering method and explicit transitions.</td><td>Documented</td><td><a href="https://github.com/kemiller2002/ordo/tree/2df928ae4618eb075d226ff307843827486ffe39">Source</a></td></tr>
<tr><th scope="row">Forma</th><td>Semantic web presentation and diagram contracts.</td><td>Verified in this build</td><td><a href="https://github.com/kemiller2002/forma/tree/18e6ff7b15e54862521a101ac2589da0c597fc98">Source</a></td></tr>
<tr><th scope="row">Folio</th><td>Printable document primitives and renderer tiers.</td><td>Verified in this build</td><td><a href="https://github.com/kemiller2002/folio/tree/a94cea73dd4b4c5409fb0d4399ba65f7dbc51f5e">Source</a></td></tr>
<tr><th scope="row">Limen</th><td>Optional browser and WebAssembly boundary.</td><td>Documented; not used here</td><td><a href="https://github.com/kemiller2002/limen/tree/ddfc1e877cff1bbcb585952289f8eeae3062addd">Source</a></td></tr>
<tr><th scope="row">Visual Engineering</th><td>UI evidence, accessibility, and conformance standards.</td><td>Documented</td><td><a href="https://github.com/kemiller2002/visual-engineering/tree/1bda5ff24493d75af02e02df4ca11fe1a417fc5c">Source</a></td></tr>
<tr><th scope="row">Dokimos</th><td>Longitudinal quality evidence; it explicitly leaves process telemetry to Praxis and application state legality to Ordo.</td><td>Documented boundary</td><td><a href="https://github.com/kemiller2002/dokimos/tree/bf6a62bb6246177c30cc64af35921a300b7ddf9a">Source</a></td></tr>
<tr><th scope="row">Causa</th><td>Root-cause investigation domain; canonical data, storage contract, fixtures, and a pinned F# engine.</td><td>Documented</td><td><a href="https://github.com/kemiller2002/causa-data/tree/22260f429501d0d6963fc33302ed1e64d742a374">Source</a></td></tr>
</tbody></table></ef-print-table></div>"""

        String.concat "\n"
            [ section "component-map" "D04 · Component relationships" "Required integrations and optional capability" "This repository's dependency graph is narrower than the Echelon ecosystem map: only the F# publisher, Forma, Folio, verification tools, and Pages are required." componentDiagram
              section "directory" "Component directory" "Known systems and visible unknowns" "This directory summarizes responsibilities for orientation and links to each canonical repository instead of copying its API documentation." catalog
              section "missing" "Source boundary" "Causa is documented without inventing a package" "Absence of a standalone repository is not evidence that the domain does not exist." """<aside class="ef-callout" data-ef-callout="caution" aria-labelledby="unknown-component"><p class="ef-callout__label" id="unknown-component">Qualified source</p><p>No public <code>kemiller2002/causa</code> repository was available at review time. Causa is described from its canonical datastore, pinned F# engine, Indy Init requirements, and Rehearsal 4 implementation.</p></aside>""" ]

    let private researchContent =
        let model =
            cards "Research reporting model" "3"
                [ card "Observed" "Direct evidence" "A measurement, artifact, command result, or source statement that can be inspected." (Some(statusBadge Verified)) None
                  card "Interpreted" "Evidence-bounded conclusion" "A reasoned conclusion that names the observations it relies on and preserves limitations." (Some(statusBadge Documented)) None
                  card "Hypothesized" "Testable uncertainty" "A claim that still requires an experiment and must not be presented as a finding." (Some(statusBadge Experimental)) None ]

        let rehearsalFour =
            """<ef-print-integrity status="partial"><h3>Rehearsal 4 — computed FAILED</h3><dl><div><dt>Source</dt><dd><a href="https://github.com/kemiller2002/indy-init-rehearsal-004/tree/b6947b8f69ee16e9e8e10f706b15bd1c4d22f9b1">b6947b8f…</a>; deployed candidate <code>ab08b764…</code></dd></div><div><dt>Elapsed</dt><dd>296 of 420 minutes</dd></div><div><dt>P0 requirements</dt><dd>1 PASS · 9 FAIL · 98 NOT_RUN</dd></div><div><dt>Candidate checks</dt><dd>156 PASS · 4 FAIL</dd></div><div><dt>Gate state</dt><dd>G0, G3, G9 PASS · G5, G8 FAIL · five NOT_RUN</dd></div><div><dt>Tokens and cost</dt><dd>Not measured</dd></div></dl><p data-limitations>The live core write path worked, but the mandatory scope was not accepted. D-HIST-WASM remained open: three historical cases replayed individually but failed when loaded together.</p></ef-print-integrity>
<div class="site-table-wrap" role="region" aria-label="Rehearsal 4 evidence classification" tabindex="0"><ef-print-table><table><colgroup><col style="width: 18%"><col style="width: 82%"></colgroup><caption>Rehearsal 4 claims by evidence state.</caption><thead><tr><th scope="col">State</th><th scope="col">Claim</th></tr></thead><tbody><tr><th scope="row">Observed</th><td>The independent verifier computed FAILED from a revision-bound ledger and content-addressed evidence. A token entered failed Playwright traces; it was rotated, the traces were deleted, and the upload cause was fixed.</td></tr><tr><th scope="row">Interpreted</th><td>The core path was demonstrated, but 107 of 108 P0 requirements lacked passing evidence; this is not competition readiness.</td></tr><tr><th scope="row">Hypothesized</th><td>Local reproducible WASM diagnosis and proving the acceptance schema in the first hour may reduce late CI and evidence work. A comparable run has not yet tested that claim.</td></tr></tbody></table></ef-print-table></div>"""

        let rehearsalFive =
            """<aside class="ef-callout" data-ef-callout="caution" aria-labelledby="r5-state"><p class="ef-callout__label" id="r5-state">Rehearsal 5 · NOT RUN</p><p><a href="https://github.com/kemiller2002/indy-init-rehearsal-005/tree/52e9a74c870804eb72936439fbf79c651ef348c1">Revision 52e9a74c…</a> contains only an initial commit and a one-line README. It publishes no objective, run clock, test result, token measurement, acceptance ledger, report, or verdict. Missing evidence is not zero, failure, or success.</p></aside>"""

        String.concat "\n"
            [ section "evidence-model" "Research and experiments" "Evidence maturity is part of the content" "Results are useful only when objective, protocol, measurement, failure, limitation, and source revision stay connected." model
              section "rehearsal-four" "Published experiment" "Rehearsal 4: useful core, failed acceptance" "The summary retains both the demonstrated live path and the computed failure. It does not convert partial success into acceptance." rehearsalFour
              section "rehearsal-five" "Missing evidence" "Rehearsal 5 cannot yet support comparison" "The repository state is reported directly and without speculation." rehearsalFive ]

    let private tourContent =
        let tour =
            """<ol class="ef-timeline" aria-label="Five-minute guided tour">
<li class="ef-timeline__item" id="tour-requirement"><div class="ef-timeline__marker" aria-hidden="true"></div><div class="ef-timeline__content"><p class="ef-eyebrow">Minute 1</p><h2>State the requirement</h2><p>Begin with the human-defined objective, source authority, constraints, and measurable acceptance criteria.</p><a href="../process/#lifecycle">Inspect the lifecycle</a></div></li>
<li class="ef-timeline__item" id="tour-boundary"><div class="ef-timeline__marker" aria-hidden="true"></div><div class="ef-timeline__content"><p class="ef-eyebrow">Minute 2</p><h2>Constrain the decision space</h2><p>Separate predetermined policy, delegated technical choices, required approval, and forbidden transitions.</p><a href="../agents/#boundaries">Inspect agent boundaries</a></div></li>
<li class="ef-timeline__item" id="tour-execute"><div class="ef-timeline__marker" aria-hidden="true"></div><div class="ef-timeline__content"><p class="ef-eyebrow">Minute 3</p><h2>Execute inside explicit state</h2><p>Make bounded changes, record evidence, and preserve a recoverable handoff.</p><a href="../system/#responsibilities">Inspect system responsibilities</a></div></li>
<li class="ef-timeline__item" id="tour-verify"><div class="ef-timeline__marker" aria-hidden="true"></div><div class="ef-timeline__content"><p class="ef-eyebrow">Minute 4</p><h2>Verify the actual result</h2><p>Run build, structure, browser, accessibility, print, and deployment checks appropriate to the risk.</p><a href="../research/#evidence-model">Inspect evidence states</a></div></li>
<li class="ef-timeline__item" id="tour-accept"><div class="ef-timeline__marker" aria-hidden="true"></div><div class="ef-timeline__content"><p class="ef-eyebrow">Minute 5</p><h2>Accept, correct, or block</h2><p>Record PASS, FAIL, BLOCKED, or NOT RUN. A claim changes state only when its evidence supports the transition.</p><a href="../reference/#acceptance">Inspect acceptance gates</a></div></li>
</ol>"""

        section "walkthrough" "Guided tour" "One requirement, five minutes, no hidden runtime" "Every step is semantic HTML, keyboard reachable, printable, and available when scripts or WebAssembly are unavailable." tour

    let private referenceContent =
        let sources =
            """<ol class="ef-index" aria-label="Primary source authorities">
<li class="ef-index__item"><p class="ef-index__marker">S / 01</p><div class="ef-index__heading"><p class="ef-eyebrow">Brand</p><h3 class="ef-index__title">Echelon Foundry corporate website</h3></div><p class="ef-index__summary">Commit 8c6b45358fb1bcae4f612534637077d35dd72762. Typography, palette, header, card, spacing, footer, and responsive reference.</p><a class="ef-index__link" href="https://github.com/kemiller2002/echelon-foundry">Open source</a></li>
<li class="ef-index__item"><p class="ef-index__marker">S / 02</p><div class="ef-index__heading"><p class="ef-eyebrow">Web presentation</p><h3 class="ef-index__title">Forma 0.9.0</h3></div><p class="ef-index__summary">Pinned npm package and immutable marketing/diagram contracts.</p><a class="ef-index__link" href="https://github.com/kemiller2002/forma/releases/tag/v0.9.0">Open release</a></li>
<li class="ef-index__item"><p class="ef-index__marker">S / 03</p><div class="ef-index__heading"><p class="ef-eyebrow">Print</p><h3 class="ef-index__title">Folio 0.4.1</h3></div><p class="ef-index__summary">Pinned release tarball with passive light-DOM elements and Letter print rules.</p><a class="ef-index__link" href="https://github.com/kemiller2002/folio/releases/tag/v0.4.1">Open release</a></li>
<li class="ef-index__item"><p class="ef-index__marker">S / 04</p><div class="ef-index__heading"><p class="ef-eyebrow">Architecture</p><h3 class="ef-index__title">Versioned source inventory</h3></div><p class="ef-index__summary">Exact revisions and the qualified relationship used for Praxis, Ordo, Dokimos, Causa, Limen, Visual Engineering, Forma, and Folio.</p><a class="ef-index__link" href="https://github.com/kemiller2002/echelon-engineering-system/blob/main/docs/source-inventory.md">Open inventory</a></li>
<li class="ef-index__item"><p class="ef-index__marker">S / 05</p><div class="ef-index__heading"><p class="ef-eyebrow">Experiments</p><h3 class="ef-index__title">Rehearsal evidence</h3></div><p class="ef-index__summary">Observed, interpreted, and hypothesized statements for Rehearsals 4 and 5, with primary source links.</p><a class="ef-index__link" href="https://github.com/kemiller2002/echelon-engineering-system/blob/main/docs/rehearsal-evidence.md">Open evidence review</a></li>
</ol>"""

        let gates =
            """<div class="site-table-wrap" role="region" aria-label="Acceptance gate status" tabindex="0"><ef-print-table><table><caption>Verification status is updated only by executed evidence.</caption><thead><tr><th scope="col">Gate</th><th scope="col">Requirement</th><th scope="col">Status</th></tr></thead><tbody>
<tr><th scope="row">G01-G04</th><td>F# build, corporate identity, Forma, Folio</td><td>PASS</td></tr>
<tr><th scope="row">G05-G06</th><td>Committed lockfile and clean-checkout reproduction</td><td>PASS</td></tr>
<tr><th scope="row">G07-G09</th><td>Responsive browser, accessibility, and print</td><td>PASS</td></tr>
<tr><th scope="row">G10</th><td>GitHub Pages deployment</td><td>PASS</td></tr>
<tr><th scope="row">G11-G13</th><td>Four diagrams, source-linked rehearsal evidence, verification report</td><td>PASS</td></tr>
</tbody></table></ef-print-table></div>"""

        String.concat "\n"
            [ section "sources" "Reference" "Source authority and versioned limitations" "The website is an explanatory layer. Canonical repositories continue to own their contracts and evidence." sources
              section "acceptance" "Acceptance" "Gates remain truthful while work is in progress" "A committed implementation is not sufficient evidence of successful deployment or accessibility." gates
              section "limitations" "Known limitations" "Unresolved conditions are part of the documentation" "Unavailable evidence stays visible even when the surrounding implementation is complete." """<aside class="ef-callout" data-ef-callout="caution" aria-labelledby="limitations-note"><p class="ef-callout__label" id="limitations-note">Current limitation</p><p>Rehearsal 5 has no published run evidence, so no cross-rehearsal efficiency claim is possible. This is a source-evidence limitation; all thirteen website acceptance gates are verified.</p></aside>""" ]

    let pages =
        [ { Slug = ""
            NavLabel = "Overview"
            Eyebrow = "Echelon engineering system"
            Title = "Engineering what makes AI-built software trustworthy"
            Summary = "A permanent, evidence-led guide to Echelon Foundry's architecture, methodology, components, decision boundaries, and research."
            Content = homeContent }
          { Slug = "system"
            NavLabel = "Architecture"
            Eyebrow = "System architecture"
            Title = "A system of bounded responsibilities"
            Summary = "Understand what each component owns, how relationships are evidenced, and where integrations remain optional or unknown."
            Content = systemContent }
          { Slug = "process"
            NavLabel = "Lifecycle"
            Eyebrow = "Engineering lifecycle"
            Title = "From requirement to verified implementation"
            Summary = "A constraint-driven path through planning, execution, verification, evidence, acceptance, and recovery."
            Content = processContent }
          { Slug = "agents"
            NavLabel = "Agents"
            Eyebrow = "Agent architecture"
            Title = "Capability does not erase authority"
            Summary = "Human policy, predetermined constraints, agent discretion, invalid transitions, verification gates, and approval boundaries remain explicit."
            Content = agentsContent }
          { Slug = "components"
            NavLabel = "Components"
            Eyebrow = "Component directory"
            Title = "One bounded job per system"
            Summary = "A source-linked inventory that keeps verified responsibilities separate from conceptual adjacency and unresolved integrations."
            Content = componentsContent }
          { Slug = "research"
            NavLabel = "Research"
            Eyebrow = "Research and experiments"
            Title = "Observed, interpreted, or still hypothesized"
            Summary = "Research claims carry their method, evidence state, failure conditions, limitations, and source revisions."
            Content = researchContent }
          { Slug = "tour"
            NavLabel = "Guided tour"
            Eyebrow = "Five-minute guided tour"
            Title = "Follow one requirement through the system"
            Summary = "A keyboard-accessible, no-WASM walkthrough from human intent to a verified result."
            Content = tourContent }
          { Slug = "reference"
            NavLabel = "Reference"
            Eyebrow = "Reference documentation"
            Title = "Sources, versions, gates, and limitations"
            Summary = "Inspect the source authorities, compatibility choices, verification states, and known unknowns behind this website."
            Content = referenceContent } ]

    let private rootFor slug = if String.IsNullOrEmpty slug then "" else "../"

    let private hrefFor currentSlug targetSlug =
        let root = rootFor currentSlug
        if String.IsNullOrEmpty targetSlug then root else root + targetSlug + "/"

    let private navigation currentSlug =
        pages
        |> List.map (fun page ->
            let current = if page.Slug = currentSlug then " aria-current=\"page\"" else ""
            let variant = if page.Slug = "tour" then " data-ef-variant=\"action\"" else ""
            $"<li><a class=\"ef-site-nav__link\" href=\"{hrefFor currentSlug page.Slug}\"{current}{variant}>{encode page.NavLabel}</a></li>")
        |> String.concat ""

    let private renderPage page =
        let root = rootFor page.Slug
        let sourceUrl = "https://github.com/kemiller2002/echelon-engineering-system"

        $"""<!doctype html>
<html lang="en" data-ef-theme="light">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <meta name="description" content="{encode page.Summary}">
  <meta name="color-scheme" content="light">
  <title>{encode page.Title} | Echelon Engineering</title>
  <link rel="preconnect" href="https://fonts.googleapis.com">
  <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
  <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=IBM+Plex+Mono:wght@400;500&amp;family=Manrope:wght@400;500;600;700&amp;family=Newsreader:opsz,wght@6..72,500;6..72,650&amp;display=swap">
  <link rel="stylesheet" href="{root}assets/folio/print.css">
  <link rel="stylesheet" href="{root}assets/forma/forma-all.css">
  <link rel="stylesheet" href="{root}assets/forma/forma-marketing-theme-echelon.css">
  <link rel="stylesheet" href="{root}styles.css">
  <script type="module" src="{root}assets/folio/register.js"></script>
</head>
<body class="ef-site" data-ef-layout="documentation">
  <a class="ef-skip-link" href="#main-content">Skip to main content</a>
  <header class="ef-site-header">
    <div class="ef-site-header__inner">
      <div class="ef-site-header__identity">
        <a class="ef-site-header__brand" href="{hrefFor page.Slug ""}"><span class="ef-site-header__mark" aria-hidden="true">EF</span><span class="ef-site-header__name">Echelon / Engineering</span></a>
        <p class="ef-site-header__tagline">Systems architecture &amp; verified practice</p>
      </div>
      <nav class="ef-site-nav" aria-label="Primary"><ul class="ef-site-nav__list">{navigation page.Slug}</ul></nav>
    </div>
  </header>
  <main class="ef-site__main" id="main-content" tabindex="-1">
    <ef-print-document>
      <ef-print-header class="site-print-only" repeat="page"><span slot="left">Echelon / Engineering</span><span slot="right">{encode page.NavLabel}</span></ef-print-header>
      <section class="ef-hero" aria-labelledby="page-title">
        <div class="ef-hero__content">
          <p class="ef-eyebrow">{encode page.Eyebrow}</p>
          <h1 class="ef-hero__title" id="page-title">{encode page.Title}</h1>
          <p class="ef-lead">{encode page.Summary}</p>
          <ul class="ef-badge-list" aria-label="Document qualities"><li><span class="ef-badge">F# generated</span></li><li><span class="ef-badge">Static first</span></li><li><span class="ef-badge">Printable</span></li></ul>
        </div>
        <div class="ef-hero__aside"><blockquote class="ef-statement"><p>Claims change state only when their evidence supports the transition.</p></blockquote></div>
      </section>
      {page.Content}
      <ef-print-footer class="site-print-only" repeat="page"><span slot="left">echelonfoundry.com</span><span slot="right"><ef-print-page-number format="page-of-pages">Page number supplied by the PDF renderer</ef-print-page-number></span></ef-print-footer>
    </ef-print-document>
  </main>
  <footer class="ef-site-footer" data-ef-tone="inverse">
    <div class="ef-site-footer__inner">
      <div class="ef-site-footer__main"><div><p class="ef-eyebrow">Echelon / Engineering</p><p>Architecture, methodology, experiments, and evidence for trustworthy engineering systems.</p></div><div class="ef-actions"><a class="ef-button" href="{hrefFor page.Slug "tour"}">Take the guided tour</a><a class="ef-button" href="{sourceUrl}" target="_blank" rel="noopener noreferrer">Source repository <span aria-hidden="true">↗</span><span class="site-visually-hidden"> (opens in a new tab)</span></a></div></div>
      <p class="ef-site-footer__note"><span>&copy; 2026 Echelon Foundry</span><span>Indianapolis, Indiana</span></p>
    </div>
  </footer>
</body>
</html>"""

    let private styles =
        """/* Site-specific composition only. Forma owns the shell, typography, navigation, cards, grids, focus, and responsive system. Folio owns print primitives. */
.site-print-only { display: none; }
.site-visually-hidden { position: absolute; inline-size: 1px; block-size: 1px; padding: 0; margin: -1px; overflow: hidden; clip-path: inset(50%); white-space: nowrap; border: 0; }
.site-table-wrap { max-inline-size: 100%; overflow-x: auto; overscroll-behavior-inline: contain; }
.site-table-wrap table { min-inline-size: min(42rem, 100%); }
.site-table-wrap:focus-visible { outline: var(--ef-focus-ring-width) solid var(--ef-color-focus-ring); outline-offset: var(--ef-focus-ring-offset); }
ef-print-finding, ef-print-integrity { max-inline-size: var(--ef-layout-measure); }
@media print {
  :root { --ef-print-page-size: Letter; --ef-print-page-margin: 0.7in; --ef-print-font-family: Manrope, Arial, sans-serif; --ef-print-text: #171a18; --ef-print-muted: #3a403c; --ef-print-rule: #686d68; --ef-print-accent: #905831; --ef-print-surface: transparent; --ef-print-code-surface: transparent; }
  .ef-site-header, .ef-site-footer, .ef-skip-link, .ef-actions { display: none !important; }
  .ef-site__main { flex: none; inline-size: 100% !important; max-inline-size: none; margin-inline: 0; padding: 0; }
  ef-print-document { inline-size: 100%; }
  .site-print-only { display: grid !important; }
  .ef-hero__title { font-size: 42pt; line-height: 0.96; }
  .ef-section-heading { break-inside: avoid; }
  .ef-hero { padding-block: 0 0.3in; }
  .ef-section { padding-block: 0.22in; }
  .ef-diagram-node { padding: 0.12in; }
  .ef-diagram-node__label { font-size: 9.5pt; }
  .ef-diagram-node__kind, .ef-diagram-node__description { font-size: 7.5pt; }
  .ef-diagram__relations { display: block; }
  .ef-diagram__relations li { margin-block: 0.08in; }
  .ef-card-grid { break-inside: avoid; }
  .site-table-wrap { overflow: visible; }
  .site-table-wrap table { min-inline-size: 0; font-size: 8.5pt; }
  .site-table-wrap :where(th, td) { overflow-wrap: normal; word-break: normal; }
  a { color: inherit; }
}
@media (forced-colors: active) { .site-table-wrap:focus-visible { outline-color: CanvasText; } }
"""

    let private findRepositoryRoot () =
        let rec walk (directory: DirectoryInfo) =
            if File.Exists(Path.Combine(directory.FullName, "package.json"))
               && Directory.Exists(Path.Combine(directory.FullName, "src", "EngineeringSystem.Site")) then
                directory.FullName
            elif isNull directory.Parent then
                failwith "Could not locate the repository root."
            else
                walk directory.Parent

        walk (DirectoryInfo(Environment.CurrentDirectory))

    let private ensureSafeOutput repositoryRoot requestedOutput =
        let output = Path.GetFullPath(requestedOutput, repositoryRoot)
        let root = Path.GetPathRoot(output)
        let home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)

        if String.Equals(output, root, StringComparison.Ordinal)
           || String.Equals(output, repositoryRoot, StringComparison.Ordinal)
           || String.Equals(output, home, StringComparison.Ordinal) then
            failwith $"Refusing unsafe output directory: {output}"

        output

    let private copyRequired (source: string) (destination: string) =
        if not (File.Exists source) then
            failwith $"Required dependency asset is missing: {source}. Run npm ci."

        Directory.CreateDirectory(Path.GetDirectoryName(destination)) |> ignore
        File.Copy(source, destination, true)

    let private writePage output page =
        let folder = if String.IsNullOrEmpty page.Slug then output else Path.Combine(output, page.Slug)
        Directory.CreateDirectory(folder) |> ignore
        File.WriteAllText(Path.Combine(folder, "index.html"), renderPage page)

    let private validateGeneratedSite output =
        let expectedRoutes = pages |> List.map (fun page -> if page.Slug = "" then "index.html" else Path.Combine(page.Slug, "index.html"))
        let requiredAssets =
            [ "styles.css"
              Path.Combine("assets", "forma", "forma-all.css")
              Path.Combine("assets", "forma", "forma-marketing-theme-echelon.css")
              Path.Combine("assets", "folio", "print.css")
              Path.Combine("assets", "folio", "register.js") ]

        for relativePath in expectedRoutes @ requiredAssets do
            let fullPath = Path.Combine(output, relativePath)
            if not (File.Exists fullPath) || FileInfo(fullPath).Length = 0L then
                failwith $"Generated artifact is missing or empty: {relativePath}"

        let htmlFiles = expectedRoutes |> List.map (fun relativePath -> Path.Combine(output, relativePath))
        let hrefPattern = Regex("href=\"([^\"]+)\"", RegexOptions.Compiled)

        for htmlFile in htmlFiles do
            let html = File.ReadAllText(htmlFile)
            let h1Count = Regex.Matches(html, "<h1(?:\\s|>)", RegexOptions.IgnoreCase).Count
            let currentCount = Regex.Matches(html, "aria-current=\"page\"").Count

            if h1Count <> 1 then
                failwith $"{Path.GetRelativePath(output, htmlFile)} must contain exactly one h1; found {h1Count}."

            if currentCount <> 1 then
                failwith $"{Path.GetRelativePath(output, htmlFile)} must contain exactly one current-page navigation link; found {currentCount}."

            for item in hrefPattern.Matches(html) do
                let href = item.Groups.[1].Value
                if not (href.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                   && not (href.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase))
                   && not (href.StartsWith("#", StringComparison.Ordinal)) then
                    let pathOnly = href.Split('#').[0].Split('?').[0]
                    if pathOnly <> "" then
                        let candidate = Path.GetFullPath(pathOnly, Path.GetDirectoryName(htmlFile))
                        let resolved = if Directory.Exists candidate then Path.Combine(candidate, "index.html") else candidate
                        if not (File.Exists resolved) then
                            failwith $"Broken internal link in {Path.GetRelativePath(output, htmlFile)}: {href}"

        $"Validated {htmlFiles.Length} routes, {requiredAssets.Length} assets, headings, current navigation, and internal links."

    let generate requestedOutput =
        let repositoryRoot = findRepositoryRoot ()
        let output = ensureSafeOutput repositoryRoot requestedOutput

        if Directory.Exists output then
            Directory.Delete(output, true)

        Directory.CreateDirectory(output) |> ignore

        let formaRoot = Path.Combine(repositoryRoot, "node_modules", "@echelon-foundry", "design-system", "dist", "marketing")
        let folioRoot = Path.Combine(repositoryRoot, "node_modules", "@echelon-foundry", "print-components", "src")

        copyRequired (Path.Combine(formaRoot, "forma-all.css")) (Path.Combine(output, "assets", "forma", "forma-all.css"))
        copyRequired (Path.Combine(formaRoot, "forma-marketing-theme-echelon.css")) (Path.Combine(output, "assets", "forma", "forma-marketing-theme-echelon.css"))
        copyRequired (Path.Combine(folioRoot, "styles", "print.css")) (Path.Combine(output, "assets", "folio", "print.css"))
        copyRequired (Path.Combine(folioRoot, "components", "register.js")) (Path.Combine(output, "assets", "folio", "register.js"))

        pages |> List.iter (writePage output)
        File.WriteAllText(Path.Combine(output, "styles.css"), styles)
        File.WriteAllText(Path.Combine(output, ".nojekyll"), "")

        let result = validateGeneratedSite output
        output, result
