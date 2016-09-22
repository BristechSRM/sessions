namespace Controllers

open RestModels
open DataTransform
open Models
open MeetupEventsRepository
open System
open System.Net
open System.Net.Http
open System.Web.Http

type MeetupEventsController() = 
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
        this.Request.CreateResponse(HttpStatusCode.OK, getAll() |> Seq.map MeetupEvent.toModel)
    
    member this.Get(id : Guid) = 
        let me = get id
        match box me with
        | null -> this.Request.CreateResponse(HttpStatusCode.NotFound, "")
        | _ -> this.Request.CreateResponse(HttpStatusCode.OK, me |> MeetupEvent.toModel)
    
    member this.Post(me : MeetupEvent) = 
        let guid = me |> MeetupEvent.toEntity |> add
        //Update event with meetupEventId as well
        EventsRepository.updateField me.EventId "meetupEventId" guid |> ignore
        this.Request.CreateResponse(HttpStatusCode.Created, guid)

    member this.Delete(id : Guid) = 
        let meetupEvent = get id
        delete meetupEvent.Id |> ignore
        //Update event with removing meetupEventId as well
        EventsRepository.updateField meetupEvent.EventId "meetupEventId" None |> ignore
        this.Request.CreateResponse(HttpStatusCode.NoContent)

    member this.Patch(id : Guid, op: PatchOp) = 
        this.Request.CreateResponse(HttpStatusCode.NoContent, patch id op)
