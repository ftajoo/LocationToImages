namespace LocationToImages.Repository.Convertors
{
    public static class UserConvertor
    {
        public static DTOs.User.UserDTO ToUserDTO(this EntityFramework.Entities.User val)
        {
            return new DTOs.User.UserDTO
            {
                Id = val.Id,
                Username = val.Username,
                Firstname = val.Firstname,
                Lastname = val.Lastname,
                Password = val.Password
            };
        }

        public static EntityFramework.Entities.User ToUserEntity(this DTOs.User.UserInsertDTO val)
        {
            return new EntityFramework.Entities.User
            {
                Username = val.Username,
                Firstname = val.Firstname,
                Lastname = val.Lastname,
                Password = val.Password
            };
        }

        public static EntityFramework.Entities.UserGeoLocation ToUserGeoLocationEntity(this DTOs.Location.GeoLocationDTO val, int userId)
        {
            return new EntityFramework.Entities.UserGeoLocation
            {
                UserId = userId,
                GeoLocationId = val.Id
            };
        }
    }
}
