namespace Controllers

open System.Net
open System.Net.Http
open System.Web.Http

open ProfilesRepository

type ProfilesController() = 
    inherit ApiController()

    member x.Post(profile: Models.Profile) =
        try
            let entity = profile |> DataTransform.Profiles.toEntity
            x.Request.CreateResponse(HttpStatusCode.Created, add entity)
        with
        | ex ->
            x.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)

    member x.Get() =
        try
            x.Request.CreateResponse(HttpStatusCode.OK, ProfilesRepository.getAll())
        with
        | ex ->
            x.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)