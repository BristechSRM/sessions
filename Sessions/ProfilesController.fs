namespace Controllers

open System
open System.Net
open System.Net.Http
open System.Web.Http
open ProfilesRepository
open RestModels
open DataTransform

type ProfilesController() =
    inherit ApiController()

    let patch (id: Guid) (op: PatchOp) =
        match op.Path with 
        | "rating" -> 
            updateField id "rating" <| Int32.Parse(op.Value)
        | "bio" -> 
            updateField id "bio" op.Value
        | _ ->  raise <| Exception("Can currently only patch rating or bio for profile")   
        
    member x.Post(profile: Models.Profile) = (fun () -> profile |> Profile.toEntity |> add) |> Catch.respond x HttpStatusCode.Created 

    member x.Get() = (fun () -> getAll() |> Seq.map Profile.toModel) |> Catch.respond x HttpStatusCode.OK 

    member x.Get(id : Guid) = (fun () -> get id |> Profile.toModel) |> Catch.respond x HttpStatusCode.OK 

    member x.Patch(id: Guid, op: PatchOp) = (fun () -> patch id op) |> Catch.respond x HttpStatusCode.NoContent 
