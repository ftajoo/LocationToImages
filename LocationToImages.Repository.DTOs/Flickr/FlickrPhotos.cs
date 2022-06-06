using Newtonsoft.Json;
using System.Collections.Generic;

namespace LocationToImages.Repository.DTOs.Flickr
{
    public class FlickrPhotos
    {
        public int Page { get; set; }

        public int Pages { get; set; }

        public int Total { get; set; }

        [JsonProperty("photo")]
        public IEnumerable<PhotoSearch> Photos { get; set; }
    }
}
