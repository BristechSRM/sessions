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

let update propName newValue guid =
    // Call DB with Dapper
    getConnection().Execute("update profiles(rating) values(@rating) where id = @id", dict)