module ProfilesRepository

open System

open Dapper
open Dapper.Contrib.Extensions
open Database
open Entities

let add (profile: Profile) =
    let guid = Guid.NewGuid()
    insert { profile with Id = guid } |> ignore
    guid

let getAll () = getConnection().GetAll<Profile>()

let update (propName: string) (newValue: obj) (guid: Guid) =
    let q = sprintf "update profiles set %s = @%s where id = @id" propName propName
    getConnection().Execute(q, dict[propName, newValue; "id", box guid])

let get (profileId : Guid) = getConnection().Get<Profile>(profileId)
