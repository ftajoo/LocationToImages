using Newtonsoft.Json;

namespace LocationToImages.Repository.DTOs.Flickr
{
    public class Description
    {
        [JsonProperty("_content")]
        public string HtmlContent { get; set; }
    }
}
