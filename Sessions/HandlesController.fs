namespace Controllers

open System
open System.Net
open System.Net.Http
open System.Web.Http
open HandlesRepository
open RestModels
open DataTransform

type HandlesController() =
    inherit ApiController()

    let patch (handleId: int) (op: PatchOp) = 
        match op.Path with
        | "identifier" -> updateField handleId op.Path op.Value
        | _ ->  raise <| Exception(sprintf "Error: Patch currently does not accept: %s for handles" op.Path)   

    member x.Post(handle : Models.Handle) = (fun () -> handle |> Handle.toEntity |> add) |> Catch.respond x HttpStatusCode.Created 

    member x.Get(htype : string, identifier: string) =
        (fun () -> getByTypeAndIdentifier htype identifier |> Seq.head |> Handle.toModel) |> Catch.respond x HttpStatusCode.OK 

    member x.Get(profileId : Guid) = (fun () -> getByProfileId profileId |> Seq.map Handle.toModel) |> Catch.respond x HttpStatusCode.OK 

    member x.Get() = (fun () -> getAll() |> Seq.map Handle.toModel) |> Catch.respond x HttpStatusCode.OK 

    member x.Get(id: int) = (fun () -> get id |> Handle.toModel) |> Catch.respond x HttpStatusCode.OK

    member x.Patch(id: int, op: PatchOp) = (fun () -> patch id op) |> Catch.respond x HttpStatusCode.NoContent