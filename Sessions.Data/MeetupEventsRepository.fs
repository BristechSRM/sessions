module MeetupEventsRepository

open Dapper.Contrib.Extensions
open Database
open Entities
open System

let getAll () = getConnection().GetAll<MeetupEvent>()

let get (meetupEventId : Guid) = getConnection().Get<MeetupEvent>(meetupEventId)

let add (me: MeetupEvent) =
    let guid = Guid.NewGuid()
    insert { me with Id = guid } |> ignore
    guid

let delete (meetupEventId : Guid) = deleteById<MeetupEvent> meetupEventId

let updateField (meetupEventId : Guid) (propName : string) newValue = Database.updateField<MeetupEvent> meetupEventId propName newValue
