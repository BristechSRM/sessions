namespace Controllers

open DataTransform
open NotesRepository
open System.Net
open System.Net.Http
open System.Web.Http


type NotesController() = 
    inherit ApiController()
    
    member x.Get() = (fun () -> getAll() |> Seq.map Note.toModel) |> Catch.respond x HttpStatusCode.OK