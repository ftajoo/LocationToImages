namespace LocationToImages.Repository.Convertors
{
    public static class LocationConvertor
    {
        public static DTOs.Location.LocationDTO ToLocationDTO(this EntityFramework.Entities.Location val)
        {
            return new DTOs.Location.LocationDTO
            {
                Id = val.Id,
                Address = val.Address
            };
        }

        public static DTOs.Location.GeoLocationDTO ToGeoLocationDTO(this EntityFramework.Entities.GeoLocation val)
        {
            return new DTOs.Location.GeoLocationDTO
            {
                Address = val.Address,
                GeoCodes = val.GeoCodes,
                Id = val.Id
            };
        }

        public static EntityFramework.Entities.GeoLocation ToGeoLocationEntity(this DTOs.Location.GeoLocationDTO val)
        {
            return new EntityFramework.Entities.GeoLocation
            {
                GeoCodes = val.GeoCodes,
                Address = val.Address,
                Id = val.Id
            };
        }
    }
}
