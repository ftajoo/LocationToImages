using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocationToImages.Repository.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<DTOs.Location.LocationDTO>> GetLocationsAsync();

        Task<DTOs.Location.GeoLocationDTO> GetGeoLocationAsync(string address);

        Task<DTOs.Location.GeoLocationDTO> GetGeoLocationAsync(DTOs.Location.GeoLocationDTO geoLocationDTO);

        Task<DTOs.Location.GeoLocationDTO> InsertGeoLocationAsync(DTOs.Location.GeoLocationDTO geoLocationDTO);
    }
}
