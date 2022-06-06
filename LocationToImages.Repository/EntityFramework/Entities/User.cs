using System.Collections.Generic;

namespace LocationToImages.Repository.EntityFramework.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Password { get; set; }

        public virtual ICollection<UserGeoLocation> UserGeoLocations { get; set; }
    }
}
