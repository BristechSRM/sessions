module DataTransform

module Profiles = 

    let toEntity (profile: Models.Profile) : Entities.Profile =
        {   
            Id = profile.Id;
            Forename = profile.Forename;
            Surname = profile.Surname;
            Rating = profile.Rating;
            ImageUrl = profile.ImageUrl;
            Bio = profile.Bio
        }

    let toModel (profile: Entities.Profile) : Models.Profile =
        {   
            Id = profile.Id;
            Forename = profile.Forename;
            Surname = profile.Surname;
            Rating = profile.Rating;
            ImageUrl = profile.ImageUrl;
            Bio = profile.Bio
        }

module Handles = 
    let toEntity (handle: Models.Handle) : Entities.Handle = 
        {
            ProfileId = handle.ProfileId
            Identifier = handle.Identifier
            Type = handle.Type 
        }