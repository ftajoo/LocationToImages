namespace LocationToImages.Repository.Convertors
{
    public static class PhotoConvertor
    {
        public static DTOs.Photo.PhotoDTO ToPhotoDTO(this EntityFramework.Entities.Photo val)
        {
            return new DTOs.Photo.PhotoDTO
            {
                Address = val.Address,
                Description = val.Description,
                Id = val.Id,
                Latitude = val.Latitude,
                Longitude = val.Longitude,
                Title = val.Title,
                Url = val.Url,
            };
        }

        public static EntityFramework.Entities.Photo ToPhotoEntity(this DTOs.Photo.PhotoDTO val)
        {
            return new EntityFramework.Entities.Photo
            {
                Address = val.Address,
                Description = val.Description,
                Id = val.Id,
                Latitude = val.Latitude,
                Longitude = val.Longitude,
                Title = val.Title,
                Url = val.Url,
            };
        }

        public static EntityFramework.Entities.Photo ToPhotoEntity(this DTOs.Photo.PhotoInsertDTO val)
        {
            return new EntityFramework.Entities.Photo
            {
                Address = val.Address,
                Description = val.Description,
                Latitude = val.Latitude,
                Longitude = val.Longitude,
                Title = val.Title,
                Url = val.Url,
            };
        }

        public static DTOs.Photo.PhotoDTO ToPhotoDTO(this DTOs.Flickr.PhotoSearch val, string url)
        {
            return new DTOs.Photo.PhotoDTO
            {
                Latitude = val.Location.Latitude,
                Longitude = val.Location.Longitude,
                Description = val.Description.HtmlContent,
                Title = val.Title,
                Address = $"{val.Location.Country?.HtmlContent}, {val.Location.Region?.HtmlContent}, {val.Location.Locality?.HtmlContent}",
                Url = url
            };
        }

        public static DTOs.Photo.PhotoDTO ToPhotoDTO(this DTOs.Flickr.PhotoGetInfo val, string url)
        {
            return new DTOs.Photo.PhotoDTO
            {
                Latitude = val.Location.Latitude,
                Longitude = val.Location.Longitude,
                Description = val.Description.HtmlContent,
                Title = val.Title.HtmlContent,
                Address = $"{val.Location.Country?.HtmlContent}, {val.Location.Region?.HtmlContent}, {val.Location.Locality?.HtmlContent}",
                Url = url
            };
        }
    }
}
