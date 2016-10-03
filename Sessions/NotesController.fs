namespace Controllers

open DataTransform
open NotesRepository
open System.Net
open System.Net.Http
open System.Web.Http
open Models
open System
open RestModels


type NotesController() = 
    inherit ApiController()

    let patch (id : Guid) (op : PatchOp) = 
        match op.Path.ToLowerInvariant() with
        | "note" -> 
            updateField id "datemodified" DateTime.UtcNow |> ignore
            updateField id op.Path op.Value
        | _ -> raise <| Exception(sprintf "Error: Patch currently does not accept: %s for note" op.Path)
    
    member x.Get() = (fun () -> getAll() |> Seq.map Note.toModel) |> Catch.respond x HttpStatusCode.OK

    [<HttpGet>]
    member x.GetBySessionId(sessionId : Guid) = (fun () -> getNotesBySessionId sessionId |> Seq.map Note.toModel) |> Catch.respond x HttpStatusCode.OK

    member x.Post(note : Note) = 
        if String.IsNullOrWhiteSpace note.Note then
            x.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Empty notes are not allowed to be created.")
        else
            x.Request.CreateResponse(HttpStatusCode.Created, note |> Note.toEntity |> add)

    member x.Patch(id : Guid, op: PatchOp) = (fun () -> patch id op) |> Catch.respond x HttpStatusCode.NoContent

    member x.Delete(id : Guid) =
        let note = get id
        delete note.Id |> ignore
        x.Request.CreateResponse(HttpStatusCode.NoContent)
