module HandlesRepository

open Dapper
open Database
open System
open Entities

let add (handle : Handle) = insert handle |> ignore

let getByTypeAndIdentifier (htype : string) (identifier : string) =
    getConnection().Query<Handle> ("select profileId, type, identifier from handles where type = @Type and identifier = @Identifier",
                                            dict [ "Type", box htype
                                                   "Identifier", box identifier ])

let getByProfileId (profileId : Guid) =
    getConnection().Query<Handle> ("select profileId, type, identifier from handles where profileId = @ProfileId", dict [ "ProfileId", box profileId ])

let getAll () = getConnection().Query<Handle>("select profileId, type, identifier from handles order by type, identifier")
