namespace Controllers

open DataTransform
open NotesRepository
open System.Net
open System.Net.Http
open System.Web.Http
open Models
open System


type NotesController() = 
    inherit ApiController()
    
    member x.Get() = (fun () -> getAll() |> Seq.map Note.toModel) |> Catch.respond x HttpStatusCode.OK

    [<HttpGet>]
    member x.GetBySessionId(sessionId : Guid) = (fun () -> getNotesBySessionId sessionId |> Seq.map Note.toModel) |> Catch.respond x HttpStatusCode.OK

    member x.Post(note : Note) = (fun () -> note |> Note.toEntity |> add) |> Catch.respond x HttpStatusCode.Created