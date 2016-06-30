module HandlesRepository

open Dapper
open Database
open System
open Entities

let add (handle : Handle) = insert handle |> ignore

let getByTypeAndIdentifier (handletype : string) (identifier : string) =
    getConnection().Query<Entities.Handle> ("select profileId, type, identifier from handles where type = @Type and identifier = @Identifier",
                                            dict [ "Type", box handletype
                                                   "Identifier", box identifier ])

let getByProfileId (profileId : Guid) = getConnection().Query<Entities.Handle> ("select profileId, type, identifier from handles where profileId = @ProfileId", dict [ "ProfileId", box profileId ])
