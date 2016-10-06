namespace Controllers

open Newtonsoft.Json
open System
open System.Net
open System.Net.Http
open System.Web.Http
open ProfilesRepository
open RestModels
open DataTransform
open Models

type ProfilesController() =
    inherit ApiController()

    let patch (id: Guid) (op: PatchOp) =
        match op.Path with 
        | "bio" | "forename" | "surname" -> updateField id op.Path op.Value
        | "handles" -> 
            JsonConvert.DeserializeObject<Handle[]>(op.Value) 
            |> Array.map Handle.toEntity
            |> HandlesRepository.putHandles id
        | _ ->  raise <| Exception(sprintf "Error: Patch currently does not accept: %s for profile" op.Path)   
        
    member x.Post(profile: Profile) = (fun () -> profile |> Profile.toEntity |> add) |> Catch.respond x HttpStatusCode.Created 

    member x.Get() = (fun () -> getAll() |> Seq.map Profile.toModel) |> Catch.respond x HttpStatusCode.OK 

    member x.Get(id : Guid) = (fun () -> get id |> Profile.toModel) |> Catch.respond x HttpStatusCode.OK 

    member x.Get(isAdmin : bool) = (fun () -> getByIsAdmin isAdmin |> Seq.map Profile.toModel) |> Catch.respond x HttpStatusCode.OK

    member x.Patch(id: Guid, op: PatchOp) = (fun () -> patch id op) |> Catch.respond x HttpStatusCode.NoContent 
