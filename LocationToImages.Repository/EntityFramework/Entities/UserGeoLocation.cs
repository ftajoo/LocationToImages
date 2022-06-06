namespace LocationToImages.Repository.EntityFramework.Entities
{
    public class UserGeoLocation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int GeoLocationId { get; set; }

        public virtual User User { get; set; }

        public virtual GeoLocation GeoLocation { get; set; }
    }
}
