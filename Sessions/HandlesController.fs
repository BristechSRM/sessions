namespace Controllers

open System
open System.Net
open System.Net.Http
open System.Web.Http

open HandlesRepository
open DataTransform

type HandlesController() =
    inherit ApiController()

    member x.Post(handle : Models.Handle) = (fun () -> handle |> Handle.toEntity |> add) |> Catch.respond x HttpStatusCode.Created 

    member x.Get(htype : string, identifier: string) =
        (fun () -> getByTypeAndIdentifier htype identifier |> Seq.head |> Handle.toModel) |> Catch.respond x HttpStatusCode.OK 

    member x.Get(profileId : Guid) = (fun () -> getByProfileId profileId |> Seq.map Handle.toModel) |> Catch.respond x HttpStatusCode.OK 

    member x.Get() = (fun () -> getAll() |> Seq.map Handle.toModel) |> Catch.respond x HttpStatusCode.OK 
