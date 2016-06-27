module ProfilesRepository

open System

open Dapper
open Dapper.Contrib.Extensions
open Database

let add (profile: Entities.Profile) =
    let guid = Guid.NewGuid()
    Database.getConnection().Execute(
        @"insert profiles(id, forename, surname, rating, imageUrl, bio) values(@Id, @Forename, @Surname, @Rating, @ImageUrl, @Bio)",
        { profile with Id = guid }) |> ignore
    guid

let getAll () = Database.getConnection().GetAll<Entities.Profile>()