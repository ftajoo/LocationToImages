using Newtonsoft.Json;

namespace LocationToImages.Repository.DTOs.Flickr
{
    public class Region
    {
        [JsonProperty("_content")]
        public string HtmlContent { get; set; }
    }
}
