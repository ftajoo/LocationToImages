namespace LocationToImages.Repository.DTOs.Flickr
{
    public class PlaceSearchQuery
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int PerPage { get; set; }

        public int Page { get; set; }
    }
}
