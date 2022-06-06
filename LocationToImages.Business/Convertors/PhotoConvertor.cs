namespace LocationToImages.Business.Convertors
{
    public static class PhotoConvertor
    {
        public static DTOs.Photo.PhotoDTO ToPhotoDTO(this Repository.DTOs.Photo.PhotoDTO val)
        {
            return new DTOs.Photo.PhotoDTO
            {
                Address = val.Address,
                Description = val.Description,
                Id = val.Id,
                Latitude = val.Latitude,
                Longitude = val.Longitude,
                Title = val.Title,
                Url = val.Url
            };
        }

        public static Repository.DTOs.Photo.PhotoInsertDTO ToPhotoInsertDTO(this DTOs.Photo.PhotoDTO val)
        {
            return new Repository.DTOs.Photo.PhotoInsertDTO
            {
                Address = val.Address,
                Description = val.Description,
                Latitude = val.Latitude,
                Longitude = val.Longitude,
                Title = val.Title,
                Url = val.Url
            };
        }
        public static Repository.DTOs.Photo.PhotoDTO ToPhotoDTO(this DTOs.Photo.PhotoDTO val)
        {
            return new Repository.DTOs.Photo.PhotoDTO
            {
                Address = val.Address,
                Description = val.Description,
                Id = val.Id,
                Latitude = val.Latitude,
                Longitude = val.Longitude,
                Title = val.Title,
                Url = val.Url
            };
        }
    }
}
