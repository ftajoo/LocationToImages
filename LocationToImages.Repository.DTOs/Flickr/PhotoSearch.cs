namespace LocationToImages.Repository.DTOs.Flickr
{
    public class PhotoSearch
    {
        public string Id { get; set; }

        public string Secret { get; set; }

        public string Server { get; set; }

        public string Title { get; set; }

        public Description Description { get; set; }

        public Location Location { get; set; }
    }
}
