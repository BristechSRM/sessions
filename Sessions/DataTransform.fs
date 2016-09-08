module DataTransform

open System

module Profile = 
    let toEntity (profile : Models.Profile) : Entities.Profile = 
        { Id = profile.Id
          Forename = profile.Forename
          Surname = profile.Surname
          Rating = profile.Rating
          ImageUrl = profile.ImageUrl
          Bio = profile.Bio 
          IsAdmin = profile.IsAdmin }
    
    let toModel (profile : Entities.Profile) : Models.Profile = 
        { Id = profile.Id
          Forename = profile.Forename
          Surname = profile.Surname
          Rating = profile.Rating
          ImageUrl = profile.ImageUrl
          Bio = profile.Bio 
          IsAdmin = profile.IsAdmin }

module Handle = 
    let toEntity (handle : Models.Handle) : Entities.Handle = 
        { Id = handle.Id
          ProfileId = handle.ProfileId
          Identifier = handle.Identifier
          Type = handle.Type }

    let toModel (handle : Entities.Handle) : Models.Handle = 
        { Id = handle.Id
          ProfileId = handle.ProfileId
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
          EventId = session.EventId |> Option.toNullable
          DateAdded = 
            match session.DateAdded with
            | None -> DateTime.UtcNow
            | Some date -> date
          Date = session.Date |> Option.toNullable }

    let toModel (session : Entities.Session) : Models.Session = 
        { Id = session.Id
          Title = session.Title
          Description = session.Description
          Status = session.Status
          SpeakerId = session.SpeakerId
          AdminId = session.AdminId |> Option.ofNullable
          EventId = session.EventId |> Option.ofNullable
          DateAdded = session.DateAdded |> Some
          Date = session.Date |> Option.ofNullable }

module Event =
    let toEntity(event: Models.Event) : Entities.Event =
      { Id = event.Id
        Date = event.Date |> Option.toNullable
        Name = event.Name 
        PublishedDate = event.PublishedDate |> Option.toNullable }

    let toModel(event: Entities.Event) : Models.Event =
      { Id = event.Id
        Date = event.Date |> Option.ofNullable
        Name = event.Name 
        PublishedDate = event.PublishedDate |> Option.ofNullable }
