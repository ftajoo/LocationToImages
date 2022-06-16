using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocationToImages.Business.Interfaces
{
    public interface IPhotoService
    {
        /// <summary>
        /// Get all saved photos from database.
        /// </summary>
        /// <returns>All saved photos from database.</returns>
        Task<IEnumerable<DTOs.Photo.PhotoDTO>> GetSavedPhotosAsync();
        
        Task<IEnumerable<DTOs.Photo.PhotoDTO>> GetSavedPhotosAsync(Func<DTOs.Photo.PhotoDTO, bool> filter);

        Task<IEnumerable<DTOs.Photo.PhotoDTO>> SearchPhotosAsync(string address);

        Task<IEnumerable<DTOs.Photo.PhotoDTO>> SearchPhotosAsync(DTOs.Photo.GeoCodesDTO geoCodes);
    
        Task<IEnumerable<DTOs.Photo.PhotoDTO>> InsertPhotosAsync(IEnumerable<DTOs.Photo.PhotoDTO> photos);

        Task<IEnumerable<DTOs.Photo.PhotoDTO>> InsertPhotosAsync(string address);

        Task<IEnumerable<DTOs.Photo.PhotoDTO>> InsertPhotosAsync(DTOs.Photo.GeoCodesDTO geoCodes);
    }
}
