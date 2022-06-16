using LocationToImages.Repository.Convertors;
using LocationToImages.Repository.DTOs.Location;
using LocationToImages.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationToImages.Repository.Location
{
    public class LocationRepository : Interfaces.ILocationRepository
    {
        private readonly LocationToImagesContext locationToImagesContext;
        private readonly Interfaces.IFoursquareApiRepository foursquareApi;

        public LocationRepository(LocationToImagesContext locationToImagesContext,
            Interfaces.IFoursquareApiRepository foursquareApi)
        {
            this.locationToImagesContext = locationToImagesContext;
            this.foursquareApi = foursquareApi;
        }

        public async Task<GeoLocationDTO> GetGeoLocationAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException(nameof(address));
            }

            DTOs.Foursquare.FoursquareData foursquareData = await foursquareApi.GetFoursquareDataAsync(address, 1);

            decimal latitude = foursquareData.Context.GeoBounds.Circle.Center.Latitude;
            decimal longitude = foursquareData.Context.GeoBounds.Circle.Center.Longitude;

            return foursquareData == null
                ? null
                : new GeoLocationDTO
                {
                    Address = address,
                    GeoCodes = $"{latitude},{longitude}"
                };
        }

        public async Task<GeoLocationDTO> GetGeoLocationAsync(GeoLocationDTO geoLocationDTO)
        {
            EntityFramework.Entities.GeoLocation geoLocation = locationToImagesContext.GeoLocations
                .FirstOrDefault(g => g.GeoCodes.Equals(geoLocationDTO.GeoCodes));

            return await Task.Run(() => geoLocation?.ToGeoLocationDTO());
        }

        public async Task<IEnumerable<LocationDTO>> GetLocationsAsync()
        {
            return await Task.Run(() => locationToImagesContext.Locations.AsEnumerable().Select(l => l.ToLocationDTO()));
        }

        public async Task<GeoLocationDTO> InsertGeoLocationAsync(GeoLocationDTO geoLocationDTO)
        {
            EntityFramework.Entities.GeoLocation geoLocation = geoLocationDTO.ToGeoLocationEntity();
            locationToImagesContext.GeoLocations.Add(geoLocation);
            await locationToImagesContext.SaveChangesAsync();
            return geoLocation.ToGeoLocationDTO();
        }
    }
}
