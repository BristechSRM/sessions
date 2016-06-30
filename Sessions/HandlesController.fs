namespace Controllers

open System
open System.Net
open System.Net.Http
open System.Web.Http

open Controllers.Common
open HandlesRepository

type HandlesController() = 
    inherit ApiController()

    member x.Post(handle: Models.Handle) = Catch x HttpStatusCode.Created (fun () -> handle |> DataTransform.Handles.toEntity |> add)
   