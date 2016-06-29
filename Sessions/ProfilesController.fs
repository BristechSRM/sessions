namespace Controllers

open System
open System.Net
open System.Net.Http
open System.Web.Http

open ProfilesRepository

// TODO: Belongs in a different namespace, since potentially pertains to all controllers
type PatchOp = { Path: string; Value: string }

type ProfilesController() =
    inherit ApiController()

    let patch (id: Guid) (op: PatchOp) =
        if op.Path <> "rating" then raise <| Exception("Can currently only patch rating for profile")
        let value = Int32.Parse(op.Value)
        id |> ProfilesRepository.update "rating" value

    // TODO: Belongs in a base class or some other utility module
    member private x.Try successCode op =
        try
            x.Request.CreateResponse(successCode, op())
        with
        | ex ->
            x.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)

    member x.Post(profile: Models.Profile) =
        x.Try HttpStatusCode.Created (fun () -> profile |> DataTransform.Profiles.toEntity |> add)

    member x.Get() =
        x.Try HttpStatusCode.OK (fun () -> ProfilesRepository.getAll() |> Seq.map DataTransform.Profiles.toModel)

    member x.Get(id : Guid) =
        x.Try HttpStatusCode.OK (fun () -> ProfilesRepository.get id |> DataTransform.Profiles.toModel)

    member x.Patch(id: Guid, op: PatchOp) =
        x.Try HttpStatusCode.NoContent (fun () -> patch id op)

    member x.Get(id : Guid) =
        x.Try HttpStatusCode.OK (fun () -> ProfilesRepository.get id)
