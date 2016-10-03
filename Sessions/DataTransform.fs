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

module Note = 
    let toEntity (note : Models.Note) : Entities.Note = 
        { Id = note.Id 
          SessionId = note.SessionId
          DateAdded = note.DateAdded
          DateModified = note.DateModified
          Note = note.Note }

    let toModel (note : Entities.Note) : Models.Note = 
        { Id = note.Id 
          SessionId = note.SessionId
          DateAdded = note.DateAdded
          DateModified = note.DateModified
          Note = note.Note }
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
            | Some date -> date }

    let toModel (session : Entities.Session) : Models.Session = 
        { Id = session.Id
          Title = session.Title
          Description = session.Description
          Status = session.Status
          SpeakerId = session.SpeakerId
          AdminId = session.AdminId |> Option.ofNullable
          EventId = session.EventId |> Option.ofNullable
          DateAdded = session.DateAdded |> Some }

module Event =
    let toEntity (event: Models.Event) : Entities.Event =
      { Id = event.Id
        Date = event.Date |> Option.toNullable
        Name = event.Name 
        MeetupEventId = event.MeetupEventId |> Option.toNullable }

    let toModel (event: Entities.Event) : Models.Event =
      { Id = event.Id
        Date = event.Date |> Option.ofNullable
        Name = event.Name 
        MeetupEventId = event.MeetupEventId |> Option.ofNullable}

module MeetupEvent =
    let toEntity (me: Models.MeetupEvent) : Entities.MeetupEvent =
      { Id = me.Id 
        EventId = me.EventId
        MeetupId = me.MeetupId
        PublishedDate = me.PublishedDate |> Option.toNullable
        MeetupUrl = me.MeetupUrl }

    let toModel (me: Entities.MeetupEvent) : Models.MeetupEvent =
      { Id = me.Id 
        EventId = me.EventId
        MeetupId = me.MeetupId
        PublishedDate = me.PublishedDate |> Option.ofNullable
        MeetupUrl = me.MeetupUrl }
