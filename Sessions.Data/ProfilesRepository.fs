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

let updateField (guid : Guid) (propName : string) newValue = 
    let q = sprintf "update profiles set %s = @%s where id = @id" propName propName
    
    let result = getConnection().Execute(q, dict [ propName, box newValue; "id", box guid ])
    if result = 0 then 
        raise <| Exception("Profile was not updated. Check that the input guid matches a profile.")
    else 
        result