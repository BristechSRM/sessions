module Models

open System

[<CLIMutable>]
type Handle = 
    { ProfileId : Guid 
      Type : string
      Identifier : string }

[<CLIMutable>]
type Profile = 
    { Id : Guid
      Forename : string
      Surname : string
      Rating : int
      ImageUrl : string
      Bio : string }