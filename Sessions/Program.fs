﻿module Program

open Microsoft.Owin.Hosting
open System
open System.Configuration
open System.Threading

(*
    Do not run Visual Studio as Administrator!

    Open a command prompt as Administrator and run the following command, replacing username with your username
    netsh http add urlacl url=http://*:8080/ user=username
*)
[<EntryPoint>]
let main _ =
    try
        let baseUrl = ConfigurationManager.AppSettings.Get("BaseUrl")
        if String.IsNullOrEmpty baseUrl then
            failwith "Missing configuration value: 'BaseUrl'"

        use server = WebApp.Start<Bristech.Srm.HttpConfig.Startup>(baseUrl)

        let waitIndefinitelyWithToken = 
            let cancelSource = new CancellationTokenSource()
            cancelSource.Token.WaitHandle.WaitOne() |> ignore
        0
    with
    | ex -> 1
