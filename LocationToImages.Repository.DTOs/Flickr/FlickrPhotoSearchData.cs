namespace LocationToImages.Repository.DTOs.Flickr
{
    public class FlickrPhotoSearchData
    {
        public FlickrPhotos Photos { get; set; }

        public string Stat { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }
    }
}
