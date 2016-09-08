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
      Bio : string 
      IsAdmin : bool }

[<CLIMutable>]
type Session =
    { Id : Guid
      Title : string
      Description : string
      Status : string
      Date : DateTime option // TODO: Remove when event id connection is complete
      SpeakerId : Guid
      AdminId : Guid option
      EventId : Guid option
      DateAdded : DateTime option }

[<CLIMutable>]
type Event = 
    { Id : Guid
      Date : DateTime option
      Name : string 
      PublishedDate : DateTime option }
