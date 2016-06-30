module SessionsRepository

open System
open Dapper
open Dapper.Contrib.Extensions
open Database
open Entities

let add (session: Session) =
    let guid = Guid.NewGuid()
    insert {session with Id = guid; Date = Nullable DateTime.UtcNow;} |> ignore
    guid

let getAll () = getConnection().GetAll<Session>()

let get (sessionId : Guid) = getConnection().Get<Session>(sessionId)