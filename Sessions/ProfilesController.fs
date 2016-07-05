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
            update id "rating" <| Int32.Parse(op.Value)
        | "bio" -> 
            update id "bio" op.Value
        | _ ->  raise <| Exception("Can currently only patch rating or bio for profile")   
        
    member x.Post(profile: Models.Profile) = Catch.respond x HttpStatusCode.Created (fun () -> profile |> Profiles.toEntity |> add)

    member x.Get() = Catch.respond x HttpStatusCode.OK (fun () -> getAll() |> Seq.map Profiles.toModel)

    member x.Get(id : Guid) = Catch.respond x HttpStatusCode.OK (fun () -> get id |> Profiles.toModel)

    member x.Patch(id: Guid, op: PatchOp) = Catch.respond x HttpStatusCode.NoContent (fun () -> patch id op)
