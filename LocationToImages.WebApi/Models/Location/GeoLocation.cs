﻿namespace LocationToImages.WebApi.Models.Location
{
    public class GeoLocation
    {
        public int Id { get; set; }

        public string GeoCodes { get; set; }

        public string Address { get; set; }
    }
}
