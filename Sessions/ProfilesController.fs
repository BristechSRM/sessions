namespace Controllers

open System
open System.Net
open System.Net.Http
open System.Web.Http
open ProfilesRepository
open RestModels

type ProfilesController() =
    inherit ApiController()

    let patch (id: Guid) (op: PatchOp) =
        if op.Path <> "rating" then raise <| Exception("Can currently only patch rating for profile")
        let value = Int32.Parse(op.Value)
        id |> ProfilesRepository.update "rating" value

    member x.Post(profile: Models.Profile) =
        Catch.respond x HttpStatusCode.Created (fun () -> profile |> DataTransform.Profiles.toEntity |> add)

    member x.Get() =
        Catch.respond x HttpStatusCode.OK (fun () -> ProfilesRepository.getAll() |> Seq.map DataTransform.Profiles.toModel)

    member x.Get(id : Guid) =
        Catch.respond x HttpStatusCode.OK (fun () -> ProfilesRepository.get id |> DataTransform.Profiles.toModel)

    member x.Patch(id: Guid, op: PatchOp) =
        Catch.respond x HttpStatusCode.NoContent (fun () -> patch id op)
