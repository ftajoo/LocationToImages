namespace LocationToImages.Business.Convertors
{
    public static class UserConvertor
    {
        public static DTOs.User.UserDTO ToUserDTO(this Repository.DTOs.User.UserDTO val)
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

        public static Repository.DTOs.User.UserInsertDTO ToUserInsertDTO(this DTOs.User.UserInsertDTO val)
        {
            return new Repository.DTOs.User.UserInsertDTO
            {
                Username = val.Username,
                Firstname = val.Firstname,
                Lastname = val.Lastname,
                Password = val.Password
            };
        }
    }
}
