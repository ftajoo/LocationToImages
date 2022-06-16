using LocationToImages.Business.Convertors;
using LocationToImages.Business.DTOs.Location;
using LocationToImages.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationToImages.Business.Location
{
    internal class LocationService : Interfaces.ILocationService
    {
        private readonly ILocationRepository locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public async Task<GeoLocationDTO> GetGeoLocationAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException(nameof(address));
            }

            Repository.DTOs.Location.GeoLocationDTO geoLocation = await locationRepository.GetGeoLocationAsync(address);

            return geoLocation == null ? null : geoLocation.ToGeoLocationDTO();
        }

        public async Task<IEnumerable<LocationDTO>> GetLocationsAsync()
        {
            return (await locationRepository.GetLocationsAsync()).Select(l => l.ToLocationDTO());
        }

        public async Task<GeoLocationDTO> InsertGeoLocationAsync(string address)
        {
            Repository.DTOs.Location.GeoLocationDTO geoLocation = (await GetGeoLocationAsync(address)).ToGeoLocationDTO();

            Repository.DTOs.Location.GeoLocationDTO geoLocationDTO = await locationRepository
                .GetGeoLocationAsync(geoLocation);

            if (geoLocationDTO != null)
            {
                throw new ArgumentException("Address does not exist.");
            }

            return (await locationRepository.InsertGeoLocationAsync(geoLocation)).ToGeoLocationDTO();
        }

        public async Task<GeoLocationDTO> InsertGeoLocationAsync(GeoLocationDTO geoLocation)
        {
            if (geoLocation == null)
            {
                throw new ArgumentNullException(nameof(geoLocation));
            }

            Repository.DTOs.Location.GeoLocationDTO geoLocationDTO = await locationRepository
                .GetGeoLocationAsync(geoLocation.ToGeoLocationDTO());

            if (geoLocationDTO != null)
            {
                throw new ArgumentException(nameof(geoLocationDTO));
            }

            return (await locationRepository.InsertGeoLocationAsync(geoLocation.ToGeoLocationDTO())).ToGeoLocationDTO();
        }
    }
}
