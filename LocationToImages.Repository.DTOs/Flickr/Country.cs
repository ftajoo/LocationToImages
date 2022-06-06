using Newtonsoft.Json;

namespace LocationToImages.Repository.DTOs.Flickr
{
    public class Country
    {
        [JsonProperty("_content")]
        public string HtmlContent { get; set; }
    }
}
