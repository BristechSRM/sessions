namespace Controllers

open System.Net
open System.Net.Http
open System.Web.Http

open ProfilesRepository

type ProfilesController() = 
    inherit ApiController()

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
        x.Try HttpStatusCode.OK ProfilesRepository.getAll