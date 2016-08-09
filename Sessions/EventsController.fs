namespace Controllers

open System
open System.Net
open System.Net.Http
open System.Web.Http
open EventsRepository
open DataTransform
open Models
open System

type EventsController() = 
  inherit ApiController()

  member this.Get() =
    let events = getAll()
    this.Request.CreateResponse(HttpStatusCode.OK, events |> Seq.map Event.toModel)
