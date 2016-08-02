module ProfilesRepository

open Dapper
open Dapper.Contrib.Extensions
open Database
open Entities
open System

let add (profile : Profile) = 
    let guid = Guid.NewGuid()
    insert { profile with Id = guid } |> ignore
    guid

let getAll() = getConnection().GetAll<Profile>()

let get (profileId : Guid) = getConnection().Get<Profile> (profileId)

let getByIsAdmin (isAdmin : bool) = selectWhere<Profile> (dict [ "IsAdmin", box isAdmin ])

let updateField (guid : Guid) (propName : string) newValue = Database.updateField<Profile> guid propName newValue