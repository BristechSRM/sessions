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

let update (guid: Guid) (propName: string) (newValue: obj) = 
    let q = sprintf "update sessions set %s = @%s where id = @id" propName propName
    let result = getConnection().Execute(q, dict[propName, newValue; "id", box guid])
    if result = 0 then
        raise <| Exception("Record was not updated. Check that the input guid matches a record.")
    else 
        result