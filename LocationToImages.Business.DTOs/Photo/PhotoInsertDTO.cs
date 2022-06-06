namespace LocationToImages.Business.DTOs.Photo
{
    public class PhotoInsertDTO
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public string Address { get; set; }
    }
}
