module Startup

open Common
open Logging
open Owin
open System.Web.Http

type Startup() =
  member __.Configuration (appBuilder: IAppBuilder) =
    Logging.initialize()

    let config =
      new HttpConfiguration()
      |> Logging.configure
      |> Cors.configure
      |> Routes.configure
      |> Serialization.configure

    appBuilder.UseWebApi(config) |> ignore
