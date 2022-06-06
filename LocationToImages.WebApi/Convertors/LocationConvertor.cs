using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationToImages.WebApi.Convertors
{
    public static class LocationConvertor
    {
        public static Models.Location.GeoCodes ToGeoCodes(this Business.DTOs.Location.GeoCodesDTO val)
        {
            return new Models.Location.GeoCodes
            {
                Latitude = val.Latitude,
                Longitude = val.Longitude,
            };
        }

        public static Business.DTOs.Location.GeoLocationDTO ToGeoLocationDTO(this Models.Location.GeoLocation val)
        {
            return new Business.DTOs.Location.GeoLocationDTO
            {
                GeoCodes = val.GeoCodes,
                Id = val.Id
            };
        }

        public static Models.Location.GeoLocation ToGeoLocation(this Business.DTOs.Location.GeoLocationDTO val)
        {
            return new Models.Location.GeoLocation
            {
                GeoCodes = val.GeoCodes,
                Id = val.Id
            };
        }
    }
}