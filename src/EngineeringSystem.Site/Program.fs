namespace Echelon.EngineeringSystem

module Program =
    [<EntryPoint>]
    let main argv =
        try
            let output = if argv.Length > 0 then argv.[0] else "dist"
            let generatedPath, validation = Site.generate output
            printfn "Generated %d pages into %s" Site.pages.Length generatedPath
            printfn "%s" validation
            0
        with ex ->
            eprintfn "Site generation failed: %s" ex.Message
            1
