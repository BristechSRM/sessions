module HandlesRepository

open Dapper.Contrib.Extensions
open Database
open Entities
open System

let add (handle : Handle) = 
    insert { handle with Id = 0 } |> ignore

let getByTypeAndIdentifier (htype : string) (identifier : string) = 
    selectWhere<Handle> (dict [ "Type", box htype
                                "Identifier", box identifier ])

let getByProfileId (profileId : Guid) = selectWhere<Handle> (dict [ "ProfileId", box profileId ])

let getAll () = getConnection().GetAll<Handle>()
