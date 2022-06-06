namespace LocationToImages.Repository.DTOs.Flickr
{
    public class Location
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public Locality Locality { get; set; }

        public Region Region { get; set; }

        public Country Country { get; set; }
    }
}
