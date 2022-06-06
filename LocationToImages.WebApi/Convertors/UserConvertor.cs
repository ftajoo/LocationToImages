namespace LocationToImages.WebApi.Convertors
{
    public static class UserConvertor
    {
        public static Models.User.User ToUser(this Business.DTOs.User.UserDTO val) 
        {
            return new Models.User.User
            {
                Firstname = val.Firstname,
                Id = val.Id,
                Lastname = val.Lastname,
                Username = val.Username,
            };
        }

        public static Business.DTOs.User.UserAuthenticateDTO ToUserAuthenticateDTO(this Models.User.UserAuthenticate val)
        {
            return new Business.DTOs.User.UserAuthenticateDTO
            {
                Password = val.Password,
                Username = val.Username,
            };
        }

        public static Business.DTOs.User.UserInsertDTO ToUserInsertDTO(this Models.User.UserInsert val)
        {
            return new Business.DTOs.User.UserInsertDTO
            {
                Firstname = val.Firstname,
                Lastname = val.Lastname,
                Username = val.Username,
                Password = val.Password
            };
        }
    }
}