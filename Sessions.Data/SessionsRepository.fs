module SessionsRepository

open Dapper
open Dapper.Contrib.Extensions
open Database
open Entities
open System

let add (session : Session) = 
    let guid = Guid.NewGuid()
    insert { session with Id = guid ; DateAdded = DateTime.UtcNow } |> ignore
    guid

let getAll() = getConnection().GetAll<Session>()

let get (sessionId : Guid) = getConnection().Get<Session> (sessionId)

//TODO could improve performance by selecting only for id. 
let getIdsByEventId (eventId : Guid) = selectWhere<Session> (dict [ "EventId", box eventId ]) |> Seq.map (fun x -> x.Id)

let updateField (guid : Guid) (propName : string) newValue = Database.updateField<Session> guid propName newValue
