namespace Echelon.EngineeringSystem

open System
open System.IO
open System.Net

module Program =
    let encode (value: string) = WebUtility.HtmlEncode(value)

    type Page = { Slug: string; Title: string; Summary: string; Sections: (string * string) list }

    let pages =
        [ { Slug = ""; Title = "Engineering systems, explained"; Summary = "How requirements, constraints, execution and evidence form an inspectable engineering process."
            Sections = [ "Start here", "Explore the system architecture, follow a requirement through its lifecycle, or inspect experimental evidence. This site distinguishes verified behavior from planned capabilities."
                         "Current evidence", "This initial release is a documentation skeleton. Source contracts, brand parity and rehearsal metrics remain subject to verification." ] }
          { Slug = "system"; Title = "System architecture"; Summary = "A map of responsibilities, not an assertion of undocumented integrations."
            Sections = [ "Ordo", "State-directed engineering methodology and governed transitions."
                         "Praxis", "Repository operating system for work, provenance and evidence."
                         "Forma", "Shared semantic HTML, CSS, accessibility patterns and design tokens."
                         "Limen", "Browser boundary with a TypeScript kernel and F# WebAssembly consumers."
                         "Registry", "Capability discovery that allows optional providers to be unavailable." ] }
          { Slug = "process"; Title = "Engineering lifecycle"; Summary = "Follow a requirement through planning, execution and verification."
            Sections = [ "Define", "Specify requirements, boundaries and acceptance criteria."
                         "Execute", "Implement within explicit constraints and record evidence."
                         "Verify", "Run applicable checks and report pass, fail, blocked or not run without inventing outcomes." ] }
          { Slug = "agents"; Title = "Agent decision boundaries"; Summary = "Separate human policy, machine-enforced constraints and agent discretion."
            Sections = [ "Predetermined", "Approved requirements and invariants limit the legal decision space."
                         "Delegated", "Agents choose implementation details within their granted scope."
                         "Prohibited", "Invalid transitions must be rejected and explained." ] }
          { Slug = "components"; Title = "Component directory"; Summary = "An evidence-led inventory of the Echelon ecosystem."
            Sections = [ "Source authority", "Each component repository owns its implementation contracts and release status."
                         "Known unknowns", "Unverified interfaces are labeled unknown rather than assumed." ] }
          { Slug = "research"; Title = "Research and experiments"; Summary = "Hypotheses, methods, results and limitations."
            Sections = [ "Evidence rule", "Publish measurements only when linked to reproducible experiment artifacts."
                         "Negative results", "Failures and blocked verification gates are first-class findings." ] }
          { Slug = "indy-init"; Title = "Indy Init rehearsals"; Summary = "What the rehearsals demonstrate, and what remains unresolved."
            Sections = [ "Rehearsals", "Evidence-backed timelines and outcome comparisons will appear after the source inventory is validated."
                         "No inferred results", "Missing metrics remain not verified." ] }
          { Slug = "tour"; Title = "Five-minute guided tour"; Summary = "From a requirement to a verified result."
            Sections = [ "1. Specify", "Capture the objective and measurable acceptance criteria."
                         "2. Constrain", "Identify permissible decisions and prohibited transitions."
                         "3. Plan", "Decompose work under the governing method."
                         "4. Implement", "Produce software and durable provenance."
                         "5. Verify", "Evaluate evidence and report unresolved conditions." ] }
          { Slug = "reference"; Title = "Reference and limitations"; Summary = "Source authority and evidence status."
            Sections = [ "Status", "This is a preview. Branding, component contracts and rehearsal evidence are not yet fully audited."
                         "Accessibility", "Core content is server-generated HTML and does not depend on WebAssembly or JavaScript." ] } ]

    let nav =
        pages
        |> List.map (fun p ->
            let url = if p.Slug = "" then "./" else "./" + p.Slug + "/"
            sprintf "<a href=\"%s\">%s</a>" url (encode (if p.Slug = "" then "Overview" else p.Title)))
        |> String.concat "\n"

    let render (page: Page) =
        let depth = if page.Slug = "" then "" else "../"
        let links =
            pages
            |> List.map (fun p ->
                let path = if p.Slug = "" then depth else depth + p.Slug + "/"
                sprintf "<a href=\"%s\">%s</a>" path (encode (if p.Slug = "" then "Overview" else p.Title)))
            |> String.concat "\n"
        let sections =
            page.Sections
            |> List.map (fun (title, body) -> sprintf "<section class=\"panel\"><h2>%s</h2><p>%s</p></section>" (encode title) (encode body))
            |> String.concat "\n"
        sprintf """<!doctype html>
<html lang="en"><head><meta charset="utf-8"><meta name="viewport" content="width=device-width,initial-scale=1">
<meta name="description" content="%s"><title>%s | Echelon Engineering</title>
<link rel="stylesheet" href="%sassets/forma/all.css"><link rel="stylesheet" href="%sstyles.css"><link rel="stylesheet" media="print" href="%sassets/folio/print.css"></head>
<body class="ef-site" data-ef-theme="light"><a class="skip" href="#main">Skip to content</a><header><div class="brand">ECHELON / ENGINEERING</div><nav aria-label="Main navigation">%s</nav></header>
<main id="main"><ef-print-document><p class="eyebrow">Engineering system / Preview</p><h1>%s</h1><p class="lead">%s</p><div class="grid">%s</div></ef-print-document></main>
<footer>Documentation preview · Claims require source verification · <a href="https://github.com/kemiller2002/echelon-engineering-system">Source repository</a></footer>
</body></html>""" (encode page.Summary) (encode page.Title) depth depth depth depth links (encode page.Title) (encode page.Summary) sections

    [<EntryPoint>]
    let main argv =
        let output = if argv.Length > 0 then argv.[0] else "dist"
        Directory.CreateDirectory(output) |> ignore
        for page in pages do
            let folder = if page.Slug = "" then output else Path.Combine(output, page.Slug)
            Directory.CreateDirectory(folder) |> ignore
            File.WriteAllText(Path.Combine(folder, "index.html"), render page)
        File.WriteAllText(Path.Combine(output, "styles.css"), """/* Layout-only overrides. Forma owns foundational presentation; corporate brand parity pending verification. */
:root{--space:clamp(1rem,3vw,2rem)}
*{box-sizing:border-box}body{margin:0;font:1rem/1.6 system-ui,sans-serif}
header,main,footer{padding:var(--space);max-width:78rem;margin:auto}header{border-bottom:1px solid currentColor}.brand{font-weight:750;letter-spacing:.09em}
nav{display:flex;flex-wrap:wrap;gap:.5rem 1.2rem;margin-top:1rem}a{overflow-wrap:anywhere}
a:focus-visible{outline:3px solid currentColor;outline-offset:4px}.skip{position:absolute;left:-100vw}.skip:focus{left:1rem;top:1rem;background:white;padding:.5rem;z-index:2}
h1{font-size:clamp(2rem,5vw,4rem);line-height:1.12;max-width:20ch;overflow-wrap:anywhere}.lead{font-size:clamp(1.1rem,2vw,1.4rem);max-width:65ch}
.eyebrow{text-transform:uppercase;letter-spacing:.1em;font-weight:700}.grid{display:grid;grid-template-columns:repeat(auto-fit,minmax(min(100%,18rem),1fr));gap:1rem;margin-block:2rem}
.panel{border:1px solid currentColor;border-radius:.8rem;padding:1.5rem;min-width:0}.panel h2{line-height:1.2;overflow-wrap:anywhere}.panel p{overflow-wrap:anywhere}
footer{border-top:1px solid currentColor}@media(prefers-reduced-motion:reduce){*,*::before,*::after{animation:none!important;transition:none!important}}
""")
        printfn "Generated %d pages into %s" pages.Length output
        0
