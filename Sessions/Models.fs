module Models

open System

// Note: ClIMutable attribute should be on all records for models. Newtonsoft.Json needs it. 

[<CLIMutable>]
type Handle = 
    { Id: int
      ProfileId : Guid 
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

[<CLIMutable>]
type Session =
    { Id : Guid
      Title : string
      Description : string
      Status : string
      Date : DateTime option
      SpeakerId : Guid
      AdminId : Guid option
      DateAdded : DateTime option}
