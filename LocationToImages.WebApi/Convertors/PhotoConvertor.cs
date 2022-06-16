namespace LocationToImages.WebApi.Convertors
{
    public static class PhotoConvertor
    {
        public static Models.Photo.Photo ToPhoto(this Business.DTOs.Photo.PhotoDTO val)
        {
            return new Models.Photo.Photo
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

        public static Business.DTOs.Photo.PhotoDTO ToPhotoDTO(this Models.Photo.Photo  val)
        {
            return new Business.DTOs.Photo.PhotoDTO
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
    }
}