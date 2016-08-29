namespace Controllers

open System
open System.Net
open System.Net.Http
open System.Web.Http
open SessionsRepository
open DataTransform
open Models
open RestModels


type SessionIdController() = 
    inherit ApiController()

    member x.Get(eventId : Guid) = (fun () -> getIdsByEventId eventId) |> Catch.respond x HttpStatusCode.OK 
