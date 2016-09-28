module NotesRepository

open System
open Dapper
open Dapper.Contrib.Extensions
open Database
open Entities

let add (note : Note) = 
    let guid = Guid.NewGuid()
    insert { note with Id = guid; DateAdded = DateTime.UtcNow; DateModified = DateTime.UtcNow } |> ignore
    guid

//TODO Test method only. Remove when ready
let getAll() = getConnection().GetAll<Note>()

let getNotesBySessionId (sessionId : Guid) = selectWhere<Note> (dict [ "SessionId", box sessionId ])

let updateField (guid : Guid) (propName : string) newValue = Database.updateField<Note> guid propName newValue
