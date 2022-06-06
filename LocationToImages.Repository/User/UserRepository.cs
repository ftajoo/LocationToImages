using LocationToImages.Repository.Convertors;
using LocationToImages.Repository.DTOs.Location;
using LocationToImages.Repository.DTOs.User;
using LocationToImages.Repository.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationToImages.Repository.User
{
    public class UserRepository : Interfaces.IUserRepository
    {
        private readonly LocationToImagesContext locationToImagesContext;

        public UserRepository(LocationToImagesContext locationToImagesContext)
        {
            this.locationToImagesContext = locationToImagesContext;
        }

        public async Task<GeoLocationDTO> AddGeoLocation(int userId, GeoLocationDTO geoLocationDTO)
        {
            EntityFramework.Entities.User user = await locationToImagesContext.Users.FindAsync(userId);
            EntityFramework.Entities.GeoLocation geoLocation = await locationToImagesContext.GeoLocations.FindAsync(geoLocationDTO.Id);

            if (geoLocation == null)
            {
                geoLocation = locationToImagesContext.GeoLocations.FirstOrDefault(g => g.GeoCodes.Equals(geoLocationDTO.GeoCodes));

                if (geoLocation == null)
                {
                    geoLocation = geoLocationDTO.ToGeoLocationEntity();
                    locationToImagesContext.GeoLocations.Add(geoLocation);
                }
            }

            EntityFramework.Entities.UserGeoLocation userGeoLocation = new EntityFramework.Entities.UserGeoLocation
            {
                GeoLocation = geoLocation,
                User = user,
            };

            locationToImagesContext.UserGeoLocations.Add(userGeoLocation);

            await locationToImagesContext.SaveChangesAsync();
            return geoLocation.ToGeoLocationDTO();
        }

        public async Task DeleteGeoLocation(int userId, int geoLocationId)
        {
            EntityFramework.Entities.User user = await locationToImagesContext.Users.FindAsync(userId);
            EntityFramework.Entities.UserGeoLocation userGeoLocation = user.UserGeoLocations
                .FirstOrDefault(g => g.GeoLocation.Id == geoLocationId);
            
            if (userGeoLocation == null)
            {
                return;
            }

            locationToImagesContext.UserGeoLocations.Remove(userGeoLocation);
            await locationToImagesContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<GeoLocationDTO>> GetGeoLocations(int userId)
        {
            EntityFramework.Entities.User user = await locationToImagesContext.Users.FindAsync(userId);
            IEnumerable<EntityFramework.Entities.GeoLocation> geoLocations = user.UserGeoLocations.Select(u => u.GeoLocation).AsEnumerable();
            
            return geoLocations.Select(g => g.ToGeoLocationDTO());
        }

        public async Task<UserDTO> GetUserAsync(int id)
        {
            EntityFramework.Entities.User user = await locationToImagesContext.Users.FindAsync(id);
            return user?.ToUserDTO();
        }

        public async Task<UserDTO> GetUserAsync(string username)
        {
            EntityFramework.Entities.User user = locationToImagesContext.Users.FirstOrDefault(u => string.Equals(u.Username, username));
            return await Task.Run(() => user?.ToUserDTO());
        }

        public async Task<UserDTO> InsertUserAsync(UserInsertDTO userInsertDTO)
        {
            EntityFramework.Entities.User user = userInsertDTO.ToUserEntity();
            locationToImagesContext.Users.Add(user);
            await locationToImagesContext.SaveChangesAsync();
            return user.ToUserDTO();
        }
    }
}
