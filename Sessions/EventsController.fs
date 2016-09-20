namespace Controllers

open DataTransform
open EventsRepository
open Models
open System
open System.Net
open System.Net.Http
open System.Web.Http

type EventsController() = 
    inherit ApiController()
    
    member this.Get() = 
        let events = getAll()
        this.Request.CreateResponse(HttpStatusCode.OK, events |> Seq.map Event.toModel)
    
    member this.Get(id : Guid) = 
        let event = get id
        match box event with
        | null -> this.Request.CreateResponse(HttpStatusCode.NotFound, "")
        | _ -> this.Request.CreateResponse(HttpStatusCode.OK, event |> Event.toModel)
    
    member this.Post(event : Event) = 
        let guid = event |> Event.toEntity |> add
        this.Request.CreateResponse(HttpStatusCode.Created, guid)
