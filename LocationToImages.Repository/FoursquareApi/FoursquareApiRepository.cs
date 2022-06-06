using LocationToImages.Repository.DTOs.Foursquare;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LocationToImages.Repository.FoursquareApi
{
    public class FoursquareApiRepository : Interfaces.IFoursquareApiRepository
    {
        private readonly Interfaces.IHttpClientProvider httpClientProvider;

        public FoursquareApiRepository(Interfaces.IHttpClientProvider httpClientProvider)
        {
            this.httpClientProvider = httpClientProvider;
        }

        public string GetApiKey()
        {
            return "fsq3l4837KnFcVB6/GoW2Tt3s7k5DzQq4DV0qaFzQTvuH5Y=";
        }

        public string GetBaseURL()
        {
            return @"https://api.foursquare.com/v3/places";
        }

        public string GetPlaceSearchURL(string near, int limit)
        {
            string fields = $"fields=fsq_id{Uri.EscapeDataString(",")}geocodes{Uri.EscapeDataString(",")}location";
            return $"{GetBaseURL()}/search?{fields}&near={Uri.EscapeDataString(near)}&limit={limit}";
        }

        public async Task<FoursquareData> GetFoursquareDataAsync(string near, int limit)
        {
            Uri requestUrl = new Uri(GetPlaceSearchURL(near, limit));

            HttpResponseMessage response = await httpClientProvider.GetAsync(requestUrl.ToString(), h =>
            {
                h.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                h.TryAddWithoutValidation("authorization", GetApiKey());
            });

            if (response.IsSuccessStatusCode)
            {
                return await httpClientProvider.DeserializeObjectAsync<FoursquareData>(response);
            }

            try
            {
                // In case of error from Fousquare API, the API return an object in the following format
                // with the according HTTP status code (ex. 404):
                // {
                //      message: "Error message"
                // }
                // The following line deserializes the object, if the deserialization fails
                // then a generic error is thrown
                Error error = await httpClientProvider.DeserializeObjectAsync<Error>(response);
                throw new Exception($"Error from Foursquare.{Environment.NewLine}Code: {response.StatusCode}{Environment.NewLine}Message: {error.Message}");
            }
            catch (Exception)
            {
                throw new Exception($"Data could not be fetched from Foursquare.{Environment.NewLine}Code: {response.StatusCode}{Environment.NewLine}");
            }
        }
    }
}
