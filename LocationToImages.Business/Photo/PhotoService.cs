using LocationToImages.Business.Convertors;
using LocationToImages.Business.DTOs.Photo;
using LocationToImages.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationToImages.Business.Photo
{
    public class PhotoService : Interfaces.IPhotoService
    {
        private readonly IPhotoRepository photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            this.photoRepository = photoRepository;
        }

        public async Task<IEnumerable<PhotoDTO>> GetSavedPhotosAsync()
        {
            return (await photoRepository.GetSavedPhotosAsync()).Select(p => p.ToPhotoDTO());
        }

        public async Task<IEnumerable<PhotoDTO>> GetSavedPhotosAsync(Func<PhotoDTO, bool> filter)
        {
            return (await photoRepository.GetSavedPhotosAsync(p => filter(p.ToPhotoDTO())))
                .Select(p => p.ToPhotoDTO());
        }

        public async Task<IEnumerable<PhotoDTO>> SearchPhotosAsync(GeoCodesDTO geoCodes)
        {
            return (await photoRepository.SearchPhotosAsync(geoCodes.Latitude, geoCodes.Longitude)).Select(p => p.ToPhotoDTO());
        }

        public async Task<IEnumerable<PhotoDTO>> SearchPhotosAsync(string address)
        {
            return (await photoRepository.SearchPhotosAsync(address)).Select(p => p.ToPhotoDTO());
        }

        public async Task<IEnumerable<PhotoDTO>> InsertPhotosAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException(nameof(address));
            }

            return await InsertPhotosAsync(() => photoRepository.InsertPhotosAsync(address), 
                () => SearchPhotosAsync(address));
        }

        public async Task<IEnumerable<PhotoDTO>> InsertPhotosAsync(GeoCodesDTO geoCodes)
        {
            if (geoCodes == null)
            {
                throw new ArgumentException(nameof(geoCodes));
            }

            return await InsertPhotosAsync(() => photoRepository.InsertPhotosAsync(geoCodes.Latitude, geoCodes.Longitude), 
                () => SearchPhotosAsync(geoCodes));
        }

        private async Task<IEnumerable<PhotoDTO>> InsertPhotosAsync(
            Func<Task<IEnumerable<Repository.DTOs.Photo.PhotoDTO>>> actionInsertPhotosAsync,
            Func<Task<IEnumerable<PhotoDTO>>> funcSearchPhotosAsync)
        {
            IEnumerable<PhotoDTO> savedPhotos = await GetSavedPhotosAsync();

            if (savedPhotos == null && !savedPhotos.Any())
            {
                return (await actionInsertPhotosAsync()).Select(p => p.ToPhotoDTO());
            }

            List<PhotoDTO> newlySavedphotos = new List<PhotoDTO>();
            IEnumerable<PhotoDTO> unsavedPhotos = await funcSearchPhotosAsync();

            if (unsavedPhotos != null)
            {
                foreach (PhotoDTO photo in unsavedPhotos)
                {
                    bool foundInSavedPhotos = savedPhotos.Any(p => string.Equals(p.Url, photo.Url));

                    if (!foundInSavedPhotos)
                    {
                        newlySavedphotos.Add(photo);
                    }
                }
            }

            await photoRepository.InsertPhotosAsync(newlySavedphotos.Select(p => p.ToPhotoDTO()));

            return newlySavedphotos;
        }
    }
}
