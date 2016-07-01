module HandlesRepository

open Dapper
open Dapper.Contrib.Extensions
open Database
open Entities
open System

let add (handle : Handle) = insert handle |> ignore

let getByTypeAndIdentifier (htype : string) (identifier : string) = 
    selectWhere<Handle> (dict [ "Type", box htype
                                "Identifier", box identifier ])

let getByProfileId (profileId : Guid) = selectWhere<Handle> (dict [ "ProfileId", box profileId ])

let getAll () = getConnection().Query<Handle>("select profileId, type, identifier from handles order by type, identifier")
