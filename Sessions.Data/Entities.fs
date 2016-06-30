module Entities

open System
open Dapper.Contrib.Extensions

[<Table("handles")>]
type Handle = 
    { ProfileId : Guid 
      Type : string
      Identifier : string }

[<Table("profiles")>]
type Profile = 
    { Id : Guid
      Forename : string
      Surname : string
      Rating : int
      ImageUrl : string
      Bio : string }