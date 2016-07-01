namespace Controllers

open System
open System.Net
open System.Net.Http
open System.Web.Http
open SessionsRepository
open DataTransform
open Models


type SessionsController() = 
    inherit ApiController()

    member x.Post(session: Session) = Catch.respond x HttpStatusCode.Created (fun () -> session |> Session.toEntity |> add)

