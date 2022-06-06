namespace LocationToImages.Repository.DTOs.Flickr
{
    public class FlickrPhotoGetInfoData
    {
        public PhotoGetInfo Photo { get; set; }

        public string Stat { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }
    }
}
