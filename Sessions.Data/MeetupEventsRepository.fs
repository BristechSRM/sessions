module MeetupEventsRepository

open Dapper.Contrib.Extensions
open Database
open Entities
open System

let get (id : Guid) = getConnection().Get<MeetupEvent>(id)

let add (me: MeetupEvent) =
    let guid = Guid.NewGuid()
    insert { me with Id = guid } |> ignore
    guid
