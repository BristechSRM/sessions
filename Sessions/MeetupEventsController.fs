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
    
    member this.Get(id : Guid) = 
        let me = get id
        match box me with
        | null -> this.Request.CreateResponse(HttpStatusCode.NotFound, "")
        | _ -> this.Request.CreateResponse(HttpStatusCode.OK, me |> MeetupEvent.toModel)
    
    member this.Post(me : MeetupEvent) = 
        let guid = me |> MeetupEvent.toEntity |> add
        EventsRepository.updateField me.EventId "meetupEventId" guid |> ignore
        //Update event with MeetupId as well
        this.Request.CreateResponse(HttpStatusCode.Created, guid)
