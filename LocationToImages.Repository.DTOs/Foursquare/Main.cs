using Newtonsoft.Json;

namespace LocationToImages.Repository.DTOs.Foursquare
{
    public class Main
    {
        [JsonProperty("latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty("longitude")]
        public decimal Longitude { get; set; }
    }
}
