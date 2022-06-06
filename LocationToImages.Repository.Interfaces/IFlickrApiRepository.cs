using System.Threading.Tasks;

namespace LocationToImages.Repository.Interfaces
{
    public interface IFlickrApiRepository
    {
        string GetBaseURL();

        string GetApiKey();

        string GetPhotoSearchURL(DTOs.Flickr.PlaceSearchQuery placeSearchQuery);

        string GetPhotoGetInfoURL(string id);

        string GetPhotoURL(string server, string id, string secret);

        Task<DTOs.Flickr.FlickrPhotoSearchData> GetPhotosAsync(DTOs.Flickr.PlaceSearchQuery placeSearchQuery);

        Task<DTOs.Flickr.FlickrPhotoGetInfoData> GetPhotosInfosAsync(string id);
    }
}
