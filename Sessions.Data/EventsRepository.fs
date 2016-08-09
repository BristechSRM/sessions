module EventsRepository

open Dapper
open Dapper.Contrib.Extensions
open Database
open Entities
open System

let getAll () =
  getConnection().GetAll<Event>()
