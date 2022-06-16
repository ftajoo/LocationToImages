namespace LocationToImages.Business.Convertors
{
    public static class LocationConvertor
    {
        public static DTOs.Location.GeoCodesDTO ToGeoCodesDTO(this Repository.DTOs.Location.GeoCodesDTO val)
        {
            return new DTOs.Location.GeoCodesDTO
            {
                Latitude = val.Latitude,
                Longitude = val.Longitude
            };
        }

        public static Repository.DTOs.Location.GeoCodesDTO ToGeoCodesDTO(this DTOs.Location.GeoCodesDTO val)
        {
            return new Repository.DTOs.Location.GeoCodesDTO
            {
                Latitude = val.Latitude,
                Longitude = val.Longitude
            };
        }

        public static Repository.DTOs.Location.GeoLocationDTO ToGeoLocationDTO(this DTOs.Location.GeoLocationDTO val)
        {
            return new Repository.DTOs.Location.GeoLocationDTO
            {
                GeoCodes = val.GeoCodes,
                Id = val.Id,
                Address = val.Address
            };
        }

        public static DTOs.Location.GeoLocationDTO ToGeoLocationDTO(this Repository.DTOs.Location.GeoLocationDTO val)
        {
            return new DTOs.Location.GeoLocationDTO
            {
                GeoCodes = val.GeoCodes,
                Id = val.Id,
                Address = val.Address
            };
        }

        public static DTOs.Location.LocationDTO ToLocationDTO(this Repository.DTOs.Location.LocationDTO val)
        {
            return new DTOs.Location.LocationDTO
            {
                Id = val.Id,
                Address = val.Address
            };
        }
    }
}
