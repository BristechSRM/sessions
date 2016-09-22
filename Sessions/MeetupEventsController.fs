namespace Controllers

open DataTransform
open Models
open MeetupEventsRepository
open System
open System.Net
open System.Net.Http
open System.Web.Http

type MeetupEventsController() = 
    inherit ApiController()

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
