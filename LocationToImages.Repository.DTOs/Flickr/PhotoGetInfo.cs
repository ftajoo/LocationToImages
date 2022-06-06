namespace LocationToImages.Repository.DTOs.Flickr
{
    public class PhotoGetInfo
    {
        public string Id { get; set; }

        public string Secret { get; set; }

        public string Server { get; set; }

        public Title Title { get; set; }

        public Description Description { get; set; }

        public Location Location { get; set; }
    }
}
