namespace Controllers

open System
open System.Net
open System.Net.Http
open System.Web.Http
open SessionsRepository
open DataTransform
open Models
open RestModels


type SessionsController() = 
    inherit ApiController()

    let patch (id: Guid) (op: PatchOp) =
        match op.Path with
        | "description" | "title" -> updateField id op.Path op.Value
        | _ ->  raise <| Exception(sprintf "Error: Patch currently does not accept: %s for session" op.Path) 
        

    member x.Post(session: Session) = (fun () -> session |> Session.toEntity |> add) |> Catch.respond x HttpStatusCode.Created 

    member x.Get() = (fun () -> getAll() |> Seq.map Session.toModel) |> Catch.respond x HttpStatusCode.OK 

    member x.Get(id : Guid) = (fun () -> get id |> Session.toModel) |> Catch.respond x HttpStatusCode.OK 

    member x.Patch(id: Guid, op: PatchOp) = (fun () -> patch id op) |> Catch.respond x HttpStatusCode.NoContent 