module DataTransform.Profiles

let toEntity (profile: Models.Profile) : Entities.Profile =
    {   Id = profile.Id;
        Forename = profile.Forename;
        Surname = profile.Surname;
        Rating = profile.Rating;
        ImageUrl = profile.ImageUrl;
        Bio = profile.Bio
    }