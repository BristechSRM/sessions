namespace Controllers

open DataTransform
open EventsRepository
open Models
open RestModels
open System
open System.Net
open System.Net.Http
open System.Web.Http

type EventsController() = 
    inherit ApiController()
    
    let patch (id : Guid) (op : PatchOp) = 
        match op.Path.ToLowerInvariant() with
        | "publisheddate" -> 
            if String.IsNullOrWhiteSpace op.Value then
                updateField id op.Path None //Published date is optional, so None is allowed
            else 
                match DateTime.TryParse op.Value with
                | true, date -> updateField id op.Path date
                | false, _ -> raise <| Exception("Error: patch value could not be parsed as a DateTime. PublishedDate must be a DateTime")
        | _ -> raise <| Exception(sprintf "Error: Patch currently does not accept: %s for event" op.Path)
    
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

    member this.Patch(id : Guid, op: PatchOp) = (fun () -> patch id op) |> Catch.respond this HttpStatusCode.NoContent
