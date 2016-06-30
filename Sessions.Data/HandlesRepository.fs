module HandlesRepository

let add (handle: Entities.Handle) = Database.insert handle |> ignore
    