module HandlesRepository

open Dapper
open Database
open System

let add (handle : Entities.Handle) = insert handle |> ignore

let get (handletype : string) (identifier : string) = 
    getConnection().Query<Entities.Handle> ("select profileId, type, identifier from handles where type = @Type and identifier = @Identifier", 
                                            dict [ "Type", box handletype
                                                   "Identifier", box identifier ])