module NotesRepository

open System
open Dapper
open Dapper.Contrib.Extensions
open Database
open Entities

//TODO Test method only. Remove when ready
let getAll() = getConnection().GetAll<Note>()