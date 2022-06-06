using System.Collections.Generic;

namespace LocationToImages.Repository.EntityFramework.Entities
{
    public class GeoLocation
    {
        public int Id { get; set; }

        public string GeoCodes { get; set; }

        public string Address { get; set; }

        public virtual ICollection<UserGeoLocation> UserGeoLocations { get; set; }
    }
}
