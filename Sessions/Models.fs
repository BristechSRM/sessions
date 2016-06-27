module Models

open System

// Why is this necessary? JSON serialisation?
[<CLIMutable>]
type Profile = 
    { Id : Guid
      Forename : string
      Surname : string
      Rating : int
      ImageUrl : string
      Bio : string }