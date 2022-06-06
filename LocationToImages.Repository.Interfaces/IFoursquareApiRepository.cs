using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationToImages.Repository.Interfaces
{
    public interface IFoursquareApiRepository
    {
        string GetApiKey();

        string GetBaseURL();

        string GetPlaceSearchURL(string near, int limit);

        Task<DTOs.Foursquare.FoursquareData> GetFoursquareDataAsync(string near, int limit);
    }
}
