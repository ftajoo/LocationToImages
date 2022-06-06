using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocationToImages.Business.Interfaces
{
    public interface IUserService
    {
        Task<DTOs.User.UserDTO> GetUserAsync(int id);

        Task<DTOs.User.UserDTO> AuthenticateUserAsync(DTOs.User.UserAuthenticateDTO userAuthenticateDTO);

        Task <DTOs.User.UserDTO> InsertUserAsync(DTOs.User.UserInsertDTO userInsertDTO);
        
        Task<IEnumerable<DTOs.Location.GeoLocationDTO>> GetGeoLocations(int userId);
        
        Task<DTOs.Location.GeoLocationDTO> AddGeoLocation(int userId, DTOs.Location.GeoLocationDTO geoLocationDTO);
        
        Task DeleteGeoLocation(int userId, int geoLocationId);
    }
}
