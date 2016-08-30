namespace Controllers

open System
open System.Net
open System.Web.Http
open SessionsRepository

type SessionIdsController() = 
    inherit ApiController()

    member x.Get(eventId : Guid) = (fun () -> getIdsByEventId eventId) |> Catch.respond x HttpStatusCode.OK 
