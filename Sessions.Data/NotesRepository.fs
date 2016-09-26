module NotesRepository

open System
open Dapper
open Dapper.Contrib.Extensions
open Database
open Entities

//TODO Test method only. Remove when ready
let getAll() = getConnection().GetAll<Note>()

let getNotesBySessionId (sessionId : Guid) = selectWhere<Note> (dict [ "SessionId", box sessionId ])