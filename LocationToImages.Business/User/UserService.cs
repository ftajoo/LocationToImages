using LocationToImages.Business.DTOs.User;
using LocationToImages.Business.Convertors;
using LocationToImages.Repository.Interfaces;
using System.Threading.Tasks;
using System;
using LocationToImages.Business.DTOs.Location;
using System.Collections.Generic;
using System.Linq;

namespace LocationToImages.Business.User
{
    public class UserService : Interfaces.IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserDTO> AuthenticateUserAsync(UserAuthenticateDTO userAuthenticateDTO)
        {
            if (userAuthenticateDTO == null)
            {
                throw new ArgumentNullException(nameof(userAuthenticateDTO));
            }

            if (string.IsNullOrWhiteSpace(userAuthenticateDTO.Username))
            {
                throw new ArgumentNullException(nameof(userAuthenticateDTO.Username));
            }

            if (string.IsNullOrWhiteSpace(userAuthenticateDTO.Password))
            {
                throw new ArgumentNullException(nameof(userAuthenticateDTO.Password));
            }

            Repository.DTOs.User.UserDTO user = await userRepository.GetUserAsync(userAuthenticateDTO.Username);
            
            if (user == null)
            {
                return null;
            }

            if (!userAuthenticateDTO.Password.Equals(user.Password))
            {
                throw new ArgumentException("Incorrect password");
            }

            return user.ToUserDTO();
        }

        public async Task<UserDTO> GetUserAsync(int id)
        {
            Repository.DTOs.User.UserDTO user = await userRepository.GetUserAsync(id);
            return user?.ToUserDTO();
        }

        public async Task<UserDTO> InsertUserAsync(UserInsertDTO userInsertDTO)
        {
            if (userInsertDTO == null)
            {
                throw new ArgumentNullException(nameof(userInsertDTO));
            }

            if (string.IsNullOrWhiteSpace(userInsertDTO.Username))
            {
                throw new ArgumentNullException(nameof(userInsertDTO.Username));
            }

            if (string.IsNullOrWhiteSpace(userInsertDTO.Firstname))
            {
                throw new ArgumentNullException(nameof(userInsertDTO.Firstname));
            }

            if (string.IsNullOrWhiteSpace(userInsertDTO.Lastname))
            {
                throw new ArgumentNullException(nameof(userInsertDTO.Lastname));
            }

            if (string.IsNullOrWhiteSpace(userInsertDTO.Password))
            {
                throw new ArgumentNullException(nameof(userInsertDTO.Password));
            }

            Repository.DTOs.User.UserDTO user = await userRepository.GetUserAsync(userInsertDTO.Username);

            if (user != null)
            {
                throw new ArgumentException($"User ({user.Username}) already exists.");
            }

            return (await userRepository.InsertUserAsync(userInsertDTO.ToUserInsertDTO())).ToUserDTO();
        }

        public async Task<GeoLocationDTO> AddGeoLocation(int userId, GeoLocationDTO geoLocationDTO)
        {
            UserDTO user = await GetUserAsync(userId);

            if (user == null)
            {
                throw new ArgumentException($"User with id ({userId}) deos not exist.");
            }

            if (geoLocationDTO == null)
            {
                throw new ArgumentNullException(nameof(geoLocationDTO));
            }

            if (string.IsNullOrWhiteSpace(geoLocationDTO.GeoCodes))
            {
                throw new ArgumentNullException(nameof(geoLocationDTO.GeoCodes));
            }

            string[] geoCodes;

            try
            {
                geoCodes = geoLocationDTO.GeoCodes.Split(',');
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid geocodes.");
            }

            if (geoCodes.Length != 2)
            {
                throw new ArgumentException("Invalid geocodes.");
            }

            if (!decimal.TryParse(geoCodes[0], out _))
            {
                throw new ArgumentException("Invalid geocodes (latitude).");
            }

            if (!decimal.TryParse(geoCodes[1], out _))
            {
                throw new ArgumentException("Invalid geocodes (longitude).");
            }

            IEnumerable<GeoLocationDTO> geoLocations = (await userRepository.GetGeoLocations(userId)).Select(g => g.ToGeoLocationDTO());
            if (geoLocations.Any(g => g.GeoCodes.Equals(geoLocationDTO.GeoCodes)))
            {
                throw new ArgumentException("GeoLocations already associated.");
            }

            return (await userRepository.AddGeoLocation(userId, geoLocationDTO.ToGeoLocationDTO())).ToGeoLocationDTO();
        }

        public async Task<IEnumerable<GeoLocationDTO>> GetGeoLocations(int userId)
        {
            UserDTO user = await GetUserAsync(userId);

            if (user == null)
            {
                throw new ArgumentException($"User with id ({userId}) deos not exist.");
            }

            return (await userRepository.GetGeoLocations(userId)).Select(g => g.ToGeoLocationDTO());
        }

        public async Task DeleteGeoLocation(int userId, int geoLocationId)
        {
            UserDTO user = await GetUserAsync(userId);

            if (user == null)
            {
                throw new ArgumentException($"User with id ({userId}) deos not exist.");
            }

            await userRepository.DeleteGeoLocation(userId, geoLocationId);
        }
    }
}
