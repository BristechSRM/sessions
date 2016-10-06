module HandlesRepository

open Dapper.Contrib.Extensions
open Database
open Entities
open System

let add (handle : Handle) = 
    insert { handle with Id = 0 }

let getByTypeAndIdentifier (htype : string) (identifier : string) = 
    selectWhere<Handle> (dict [ "Type", box htype
                                "Identifier", box identifier ])

let getByProfileId (profileId : Guid) = selectWhere<Handle> (dict [ "ProfileId", box profileId ])

let getAll () = getConnection().GetAll<Handle>()

let get (handleId : int) = getConnection().Get<Handle>(handleId)

let delete (handleId : int) = deleteById<Handle> handleId

let updateField (handleId: int) (propName: string) newValue = Database.updateField<Handle> handleId propName newValue 

let putHandles (profileId : Guid) (editedHandles : Handle []) = 
    let oldHandles = getByProfileId profileId |> Seq.toArray

    let newHandles = 
        editedHandles 
        |> Array.filter (fun h -> oldHandles |> Array.exists(fun oldH -> h.Id = oldH.Id ) |> not)

    let updatedHandles = 
        editedHandles 
        |> Array.filter (fun h -> 
            let found = oldHandles |> Array.tryFind(fun oldH -> h.Id = oldH.Id )
            match found with
            | None -> false
            | Some oldH -> h.Type <> oldH.Type || h.Identifier <> oldH.Identifier)

    let deletedHandles = 
        oldHandles
        |> Array.filter (fun oldH -> editedHandles |> Array.exists(fun newH -> oldH.Id = newH.Id) |> not)

    newHandles
    |> Array.iter (fun handle -> 
        add {handle with ProfileId = profileId} |> ignore)

    updatedHandles 
    |> Array.iter (fun handle -> update<Handle> {handle with ProfileId = profileId} |> ignore)


    deletedHandles 
    |> Array.iter (fun handle -> delete handle.Id |> ignore)
    0
    