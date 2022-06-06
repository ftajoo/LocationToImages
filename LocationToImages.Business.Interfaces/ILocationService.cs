using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationToImages.Business.Interfaces
{
    public interface ILocationService
    {
        Task<DTOs.Location.GeoLocationDTO> GetGeoLocationAsync(string address);

        Task<DTOs.Location.GeoLocationDTO> InsertGeoLocationAsync(string address);

        Task<DTOs.Location.GeoLocationDTO> InsertGeoLocationAsync(DTOs.Location.GeoLocationDTO geoLocation);
    }
}
