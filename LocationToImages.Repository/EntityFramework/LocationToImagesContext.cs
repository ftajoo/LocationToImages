using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LocationToImages.Repository.EntityFramework
{
    public class LocationToImagesContext : DbContext
    {
        public DbSet<Entities.User> Users { get; set; }

        public DbSet<Entities.Location> Locations { get; set; }

        public DbSet<Entities.GeoLocation> GeoLocations { get; set; }

        public DbSet<Entities.UserGeoLocation> UserGeoLocations { get; set; }

        public DbSet<Entities.Photo> Photos { get; set; }

        /// <summary>LocationToImagesContext => found in Web.Config of web api project</summary>
        public LocationToImagesContext() : base("LocationToImagesContext")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
