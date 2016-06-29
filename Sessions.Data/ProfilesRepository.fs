module ProfilesRepository

open System

open Dapper
open Dapper.Contrib.Extensions
open Database

let add (profile: Entities.Profile) =
    let guid = Guid.NewGuid()
    Database.insert { profile with Id = guid } |> ignore
    guid

let getAll () = Database.getConnection().GetAll<Entities.Profile>()

let update (propName: string) (newValue: obj) (guid: Guid) =
    let q = sprintf "update profiles set %s = @%s where id = @id" propName propName
    getConnection().Execute(q, dict[propName, newValue; "id", box guid])