module SessionsRepository

open System
open Database
open Entities

let add (session: Session) =
    let guid = Guid.NewGuid()
    insert {session with Id = guid; Date = Nullable DateTime.UtcNow;} |> ignore
    guid