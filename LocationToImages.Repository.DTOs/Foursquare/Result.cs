using Newtonsoft.Json;

namespace LocationToImages.Repository.DTOs.Foursquare
{
    public class Result
    {
        [JsonProperty("fsq_id")]
        public string FsqId { get; set; }

        [JsonProperty("geocodes")]
        public Geocodes Geocodes { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }
    }
}
