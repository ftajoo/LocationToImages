using LocationToImages.Repository.DTOs.Flickr;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LocationToImages.Repository.FlickrApi
{
    public class FlickrApiRepository : Interfaces.IFlickrApiRepository
    {
        private readonly Interfaces.IHttpClientProvider httpClientProvider;

        public FlickrApiRepository(Interfaces.IHttpClientProvider httpClientProvider)
        {
            this.httpClientProvider = httpClientProvider;
        }

        public string GetBaseURL()
        {
            return @"https://www.flickr.com/services/rest";
        }

        public string GetApiKey()
        {
            return @"d513e219297dfcfd26f74b3aa9738576";
        }

        public string GetPhotoSearchURL(PlaceSearchQuery placeSearchQuery)
        {
            return $"{GetBaseURL()}/?method=flickr.photos.search&api_key={GetApiKey()}&lat={placeSearchQuery.Latitude}&lon={placeSearchQuery.Longitude}&per_page={placeSearchQuery.PerPage}&page={placeSearchQuery.Page}&format=json&nojsoncallback=1";
        }

        public async Task<FlickrPhotoSearchData> GetPhotosAsync(PlaceSearchQuery placeSearchQuery)
        {
            Uri requestUrl = new Uri(GetPhotoSearchURL(placeSearchQuery));

            HttpResponseMessage response = await httpClientProvider.GetAsync(requestUrl.ToString(), h =>
            {
                h.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error from Flickr.{Environment.NewLine}Code: {response.StatusCode}");
            }

            FlickrPhotoSearchData flickrData = await httpClientProvider.DeserializeObjectAsync<FlickrPhotoSearchData>(response);

            if (!"ok".Equals(flickrData.Stat))
            {
                throw new Exception($"Error from Flickr.{Environment.NewLine}Flickr code: {flickrData.Code}{Environment.NewLine}Message: {flickrData.Message}");
            }

            return flickrData;
        }

        public string GetPhotoGetInfoURL(string id)
        {
            return $"{GetBaseURL()}/?method=flickr.photos.getInfo&api_key={GetApiKey()}&photo_id={id}&format=json&nojsoncallback=1";
        }

        public async Task<FlickrPhotoGetInfoData> GetPhotosInfosAsync(string id)
        {
            Uri requestUrl = new Uri(GetPhotoGetInfoURL(id));

            HttpResponseMessage response = await httpClientProvider.GetAsync(requestUrl.ToString(), h =>
            {
                h.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error from Flickr.{Environment.NewLine}Code: {response.StatusCode}");
            }

            FlickrPhotoGetInfoData flickrData = await httpClientProvider.DeserializeObjectAsync<FlickrPhotoGetInfoData>(response);

            if (!"ok".Equals(flickrData.Stat))
            {
                throw new Exception($"Error from Flickr.{Environment.NewLine}Flickr code: {flickrData.Code}{Environment.NewLine}Message: {flickrData.Message}");
            }

            return flickrData;
        }

        public string GetPhotoURL(string server, string id, string secret)
        {
            return $"https://live.staticflickr.com/{server}/{id}_{secret}_w.jpg";
        }
    }
}
