using Newtonsoft.Json;

namespace LocationToImages.Repository.DTOs.Foursquare
{
    public class Geocodes
    {
        [JsonProperty("main")]
        public Main Main { get; set; }
    }
}