using Newtonsoft.Json;

namespace LocationToImages.Repository.DTOs.Flickr
{
    public class Title
    {
        [JsonProperty("_content")]
        public string HtmlContent { get; set; }
    }
}
