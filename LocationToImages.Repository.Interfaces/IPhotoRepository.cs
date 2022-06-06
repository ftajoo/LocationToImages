using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocationToImages.Repository.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<DTOs.Photo.PhotoDTO>> GetSavedPhotosAsync();

        Task<IEnumerable<DTOs.Photo.PhotoDTO>> GetSavedPhotosAsync(Func<DTOs.Photo.PhotoDTO, bool> filter);

        Task<IEnumerable<DTOs.Photo.PhotoDTO>> SearchPhotosAsync(string address);
        
        Task<IEnumerable<DTOs.Photo.PhotoDTO>> SearchPhotosAsync(decimal latitude, decimal longitude);

        Task<IEnumerable<DTOs.Photo.PhotoDTO>> InsertPhotosAsync(string address);

        Task<IEnumerable<DTOs.Photo.PhotoDTO>> InsertPhotosAsync(IEnumerable<DTOs.Photo.PhotoDTO> photoDTOs);

        Task<DTOs.Photo.PhotoDTO> InsertPhotoAsync(DTOs.Photo.PhotoInsertDTO photoInsertDTO);
        
        Task<IEnumerable<DTOs.Photo.PhotoDTO>> InsertPhotosAsync(decimal latitude, decimal longitude);
    }
}
