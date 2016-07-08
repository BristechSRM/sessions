#r "packages/FAKE/tools/FakeLib.dll"
open Fake

RestorePackages()

let buildDir = "./build/output"

Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "BuildApp" (fun _ ->
    !! "Sessions.sln"
    |> MSBuildRelease buildDir "Build"
    |> Log "AppBuld-Output: "
)

"Clean"
  ==> "BuildApp"

RunTargetOrDefault "BuildApp"
