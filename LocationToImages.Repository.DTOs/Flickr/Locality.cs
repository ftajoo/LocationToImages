using Newtonsoft.Json;

namespace LocationToImages.Repository.DTOs.Flickr
{
    public class Locality
    {
        [JsonProperty("_content")]
        public string HtmlContent { get; set; }
    }
}
