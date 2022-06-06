using LocationToImages.Repository.Convertors;
using LocationToImages.Repository.DTOs.Photo;
using LocationToImages.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationToImages.Repository.Photo
{
    public class PhotoRepository : Interfaces.IPhotoRepository
    {
        private readonly LocationToImagesContext locationToImagesContext;
        private readonly Interfaces.IFoursquareApiRepository foursquareApiRepository;
        private readonly Interfaces.IFlickrApiRepository flickrApiRepository;

        public PhotoRepository(LocationToImagesContext locationToImagesContext, 
            Interfaces.IFoursquareApiRepository foursquareApiRepository, 
            Interfaces.IFlickrApiRepository flickrApiRepository)
        {
            this.locationToImagesContext = locationToImagesContext;
            this.foursquareApiRepository = foursquareApiRepository;
            this.flickrApiRepository = flickrApiRepository;
        }

        public async Task<IEnumerable<PhotoDTO>> GetSavedPhotosAsync()
        {
            return await Task.Run(() => locationToImagesContext.Photos.AsEnumerable().Select(p => p.ToPhotoDTO()));
        }

        public async Task<IEnumerable<PhotoDTO>> GetSavedPhotosAsync(Func<PhotoDTO, bool> filter)
        {
            return await Task.Run(() => locationToImagesContext.Photos?
                .AsEnumerable()
                .Where(p => filter(p.ToPhotoDTO()))?
                .Select(p => p.ToPhotoDTO()));
        }

        public async Task<IEnumerable<PhotoDTO>> SearchPhotosAsync(decimal latitude, decimal longitude)
        {
            DTOs.Flickr.FlickrPhotoSearchData flickrData = await flickrApiRepository.GetPhotosAsync(new DTOs.Flickr.PlaceSearchQuery
            {
                Latitude = latitude,
                Longitude = longitude,
                Page = 1,
                PerPage = 20
            });

            if (flickrData == null)
            {
                return Enumerable.Empty<PhotoDTO>();
            }

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>();

            foreach (DTOs.Flickr.PhotoSearch photo in flickrData.Photos.Photos)
            {
                DTOs.Flickr.FlickrPhotoGetInfoData flickrPhotoGetInfoData = await flickrApiRepository.GetPhotosInfosAsync(photo.Id);

                if (flickrPhotoGetInfoData == null)
                {
                    continue;
                }

                DTOs.Flickr.PhotoGetInfo flickrPhoto = flickrPhotoGetInfoData.Photo;
                string url = flickrApiRepository.GetPhotoURL(flickrPhoto.Server, flickrPhoto.Id, flickrPhoto.Secret);
                photoDTOs.Add(flickrPhoto.ToPhotoDTO(url));
            }

            return photoDTOs;
        }

        public async Task<IEnumerable<PhotoDTO>> SearchPhotosAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException(nameof(address));
            }

            DTOs.Foursquare.FoursquareData foursquareData = await foursquareApiRepository.GetFoursquareDataAsync(address, 5);

            if (foursquareData == null)
            {
                return null;
            }

            return await SearchPhotosAsync(foursquareData.Context.GeoBounds.Circle.Center.Latitude, 
                foursquareData.Context.GeoBounds.Circle.Center.Longitude);
        }

        public async Task<IEnumerable<PhotoDTO>> InsertPhotosAsync(decimal latitude, decimal longitude)
        {
            IEnumerable<PhotoDTO> photoDTOs = await SearchPhotosAsync(latitude, longitude);
            return await InsertPhotosAsync(photoDTOs);
        }

        public async Task<IEnumerable<PhotoDTO>> InsertPhotosAsync(string address)
        {
            IEnumerable<PhotoDTO> photoDTOs = await SearchPhotosAsync(address);
            return await InsertPhotosAsync(photoDTOs);
        }

        public async Task<IEnumerable<PhotoDTO>> InsertPhotosAsync(IEnumerable<PhotoDTO> photoDTOs)
        {
            IEnumerable<EntityFramework.Entities.Photo> photos = photoDTOs.Select(p => p.ToPhotoEntity());
            locationToImagesContext.Photos.AddRange(photos);
            await locationToImagesContext.SaveChangesAsync();
            return photos.Select(p => p.ToPhotoDTO());
        }

        public async Task<PhotoDTO> InsertPhotoAsync(PhotoInsertDTO photoInsertDTO)
        {
            EntityFramework.Entities.Photo photo = photoInsertDTO.ToPhotoEntity();
            locationToImagesContext.Photos.Add(photo);
            await locationToImagesContext.SaveChangesAsync();
            return photo.ToPhotoDTO();
        }
    }
}
