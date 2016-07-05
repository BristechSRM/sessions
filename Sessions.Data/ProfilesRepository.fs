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

let get (profileId : Guid) = getConnection().Get<Profile>(profileId)

let updateField (guid: Guid) (propName: string) (newValue: obj)  =
    let q = sprintf "update profiles set %s = @%s where id = @id" propName propName
    let result = getConnection().Execute(q, dict[propName, newValue; "id", box guid])
    if result = 0 then
        raise <| Exception("Profile was not updated. Check that the input guid matches a profile.")
    else 
        result