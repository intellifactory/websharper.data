#load "tools/includes.fsx"

open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.Data")
        .VersionFrom("WebSharper")

let main =
    bt.WebSharper.Library("WebSharper.Data")
        .SourcesFromProject()
        .References(fun r ->
            [ 
                r.NuGet("FSharp.Data").Reference() 
            ])

let test =
    bt.WebSharper.Library("WebSharper.Data.Test")
        .References(fun r ->
            [ 
                r.NuGet("FSharp.Data").Reference() 
                r.NuGet("WebSharper.Charting").Reference()
                r.Project main
            ])

bt.Solution [
    main
    test

    bt.NuGet.CreatePackage()
        .Configure(fun configuration ->
            { configuration with
                Title = Some "WebSharper.Data"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://github.com/intellifactory/websharper.data"
                Description = "FSharp.Data proxies for WebSharper"
                Authors = [ "IntelliFactory" ]
                RequiresLicenseAcceptance = true })
        .Add(main)
]
|> bt.Dispatch