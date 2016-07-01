module DataTransform

open System

module Profiles = 
    let toEntity (profile : Models.Profile) : Entities.Profile = 
        { Id = profile.Id
          Forename = profile.Forename
          Surname = profile.Surname
          Rating = profile.Rating
          ImageUrl = profile.ImageUrl
          Bio = profile.Bio }
    
    let toModel (profile : Entities.Profile) : Models.Profile = 
        { Id = profile.Id
          Forename = profile.Forename
          Surname = profile.Surname
          Rating = profile.Rating
          ImageUrl = profile.ImageUrl
          Bio = profile.Bio }

module Handles = 
    let toEntity (handle : Models.Handle) : Entities.Handle = 
        { ProfileId = handle.ProfileId
          Identifier = handle.Identifier
          Type = handle.Type }

    let toModel (handle : Entities.Handle) : Models.Handle = 
        { ProfileId = handle.ProfileId
          Identifier = handle.Identifier
          Type = handle.Type }

module Session = 
    let toEntity (session : Models.Session) : Entities.Session = 
        { Id = session.Id
          Title = session.Title
          Description = session.Description
          Status = session.Status
          SpeakerId = session.SpeakerId
          AdminId = session.AdminId |> Option.toNullable
          DateAdded = 
            match session.DateAdded with
            | None -> DateTime.UtcNow
            | Some date -> date
          Date = session.Date |> Option.toNullable}