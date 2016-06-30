namespace Controllers

open System
open System.Net
open System.Net.Http
open System.Web.Http

open HandlesRepository

type HandlesController() = 
    inherit ApiController()

    member x.Post(handle: Models.Handle) = Catch.respond x HttpStatusCode.Created (fun () -> handle |> DataTransform.Handles.toEntity |> add)