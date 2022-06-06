using Newtonsoft.Json;
using System.Collections.Generic;

namespace LocationToImages.Repository.DTOs.Foursquare
{
    public class FoursquareData
    {
        public IEnumerable<Result> Results { get; set; }

        public Context Context { get; set; }
    }
}
