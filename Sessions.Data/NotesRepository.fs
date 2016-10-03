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

let get (noteId : Guid) = getConnection().Get<Note>(noteId)

let getAll() = getConnection().GetAll<Note>()

let getNotesBySessionId (sessionId : Guid) = selectWhere<Note> (dict [ "SessionId", box sessionId ])

let delete (noteId : Guid) = deleteById<Note> noteId

let updateField (guid : Guid) (propName : string) newValue = Database.updateField<Note> guid propName newValue
