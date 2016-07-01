module Models

open System
open Newtonsoft.Json 

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