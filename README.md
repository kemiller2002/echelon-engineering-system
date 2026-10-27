# echelon-engineering-system

## F# documentation website

Source: `src/EngineeringSystem.Site/`. Requires .NET 10 SDK.

```sh
dotnet build src/EngineeringSystem.Site/EngineeringSystem.Site.fsproj --configuration Release
dotnet run --project src/EngineeringSystem.Site/EngineeringSystem.Site.fsproj --configuration Release --no-build -- dist
```

Generated routes include overview, system, process, agents, components, research, Indy Init, tour and reference. GitHub Actions workflow `.github/workflows/pages.yml` builds and publishes to GitHub Pages once Pages is configured for GitHub Actions.

**Preview status:** The F# generator has been committed but not locally built or browser-tested. The current CSS is provisional, not yet a pinned Forma theme or verified match to the Echelon Foundry corporate site. Source-linked diagrams and rehearsal evidence remain to be implemented. Limen is intentionally absent from the static first release; it can be introduced for genuinely stateful interactions.
