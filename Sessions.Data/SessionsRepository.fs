module SessionsRepository

open Dapper
open Dapper.Contrib.Extensions
open Database
open Entities
open System

let add (session : Session) = 
    let guid = Guid.NewGuid()
    insert { session with Id = guid ; Date = Nullable DateTime.UtcNow } |> ignore
    guid

let getAll() = getConnection().GetAll<Session>()
let get (sessionId : Guid) = getConnection().Get<Session> (sessionId)

let updateField (guid : Guid) (propName : string) newValue = 
    let q = sprintf "update sessions set %s = @%s where id = @id" propName propName
    
    let result = getConnection().Execute(q, dict [ propName, box newValue; "id", box guid ])
    if result = 0 then 
        raise <| Exception("Session was not updated. Check that the input guid matches a session.")
    else 
        result