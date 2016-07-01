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
        if op.Path <> "description" then raise <| Exception("Can currently only patch description for session")
        update id "description" op.Value

    member x.Post(session: Session) = Catch.respond x HttpStatusCode.Created (fun () -> session |> Session.toEntity |> add)

    member x.Get() = Catch.respond x HttpStatusCode.OK (fun () -> getAll() |> Seq.map Session.toModel)

    member x.Get(id : Guid) = Catch.respond x HttpStatusCode.OK (fun () -> get id |> Session.toModel)

    member x.Patch(id: Guid, op: PatchOp) = Catch.respond x HttpStatusCode.NoContent (fun () -> patch id op)