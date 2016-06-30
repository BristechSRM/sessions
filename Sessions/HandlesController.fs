namespace Controllers

open System
open System.Net
open System.Net.Http
open System.Web.Http

open HandlesRepository
open DataTransform

type HandlesController() = 
    inherit ApiController()

    member x.Post(handle : Models.Handle) = Catch.respond x HttpStatusCode.Created (fun () -> handle |> Handles.toEntity |> add)

    member x.Get(htype : string, identifier: string) = Catch.respond x HttpStatusCode.OK (fun () -> getByTypeAndIdentifier htype identifier |> Seq.head |> Handles.toModel)

    member x.Get(profileId : Guid) = Catch.respond x HttpStatusCode.OK (fun () -> getByProfileId profileId |> Seq.head |> Handles.toModel)
