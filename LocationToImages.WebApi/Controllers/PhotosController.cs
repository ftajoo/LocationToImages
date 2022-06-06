using LocationToImages.Business.DTOs.Photo;
using LocationToImages.Business.Interfaces;
using LocationToImages.WebApi.Convertors;
using LocationToImages.WebApi.Filters;
using LocationToImages.WebApi.Models.Filter;
using LocationToImages.WebApi.Models.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace LocationToImages.WebApi.Controllers
{
    [RoutePrefix("api/photos")]
    public class PhotosController : ApiController
    {
        private readonly IPhotoService photoService;
        public PhotosController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        [JwtAuthentication]
        [HttpGet]
        [ResponseType(typeof(PagedResponse<IEnumerable<Models.Photo.Photo>>))]
        [Route()]
        public async Task<IHttpActionResult> GetAll([FromUri]PaginationFilter filter)
        {
            try
            {
                IEnumerable<PhotoDTO> response = await photoService.GetSavedPhotosAsync();
                PagedResponse<IEnumerable<Models.Photo.Photo>> pagedResponse = ApplyFilter(filter, response);

                return Ok(pagedResponse);
            }
            catch (ArgumentException ex)
            {
                PagedResponse<IEnumerable<Models.Photo.Photo>> reponse = new PagedResponse<IEnumerable<Models.Photo.Photo>>(null, 0, 0)
                {
                    Succeeded = false,
                    Error = ex.Message
                };

                return Content(System.Net.HttpStatusCode.BadRequest, reponse);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [JwtAuthentication]
        [HttpGet]
        [ResponseType(typeof(PagedResponse<IEnumerable<Models.Photo.Photo>>))]
        [Route("title")]
        public async Task<IHttpActionResult> GetByTitle(string title, [FromUri] PaginationFilter filter)
        {
            if (string.IsNullOrWhiteSpace(title) || filter == null)
            {
                return BadRequest("Invalid parameter");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                IEnumerable<PhotoDTO> photoDTOs = await photoService.GetSavedPhotosAsync(p => p.Title.Contains(title));
                PagedResponse<IEnumerable<Models.Photo.Photo>> pagedResponse = ApplyFilter(filter, photoDTOs);

                return Ok(pagedResponse);
            }
            catch (ArgumentException ex)
            {
                PagedResponse<IEnumerable<Models.Photo.Photo>> reponse = new PagedResponse<IEnumerable<Models.Photo.Photo>>(null, 0, 0)
                {
                    Succeeded = false,
                    Error = ex.Message
                };

                return Content(System.Net.HttpStatusCode.BadRequest, reponse);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [JwtAuthentication]
        [HttpGet]
        [ResponseType(typeof(PagedResponse<IEnumerable<Models.Photo.Photo>>))]
        [Route("description")]
        public async Task<IHttpActionResult> GetByDescription(string description, [FromUri] PaginationFilter filter)
        {
            if (string.IsNullOrWhiteSpace(description) || filter == null)
            {
                return BadRequest("Invalid parameter");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                IEnumerable<PhotoDTO> photoDTOs = await photoService.GetSavedPhotosAsync(p => p.Description.Contains(description));
                PagedResponse<IEnumerable<Models.Photo.Photo>> pagedResponse = ApplyFilter(filter, photoDTOs);

                return Ok(pagedResponse);
            }
            catch (ArgumentException ex)
            {
                PagedResponse<IEnumerable<Models.Photo.Photo>> reponse = new PagedResponse<IEnumerable<Models.Photo.Photo>>(null, 0, 0)
                {
                    Succeeded = false,
                    Error = ex.Message
                };

                return Content(System.Net.HttpStatusCode.BadRequest, reponse);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [JwtAuthentication]
        [HttpGet]
        [ResponseType(typeof(PagedResponse<IEnumerable<Models.Photo.Photo>>))]
        [Route("address")]
        public async Task<IHttpActionResult> GetByAddress(string address, [FromUri] PaginationFilter filter)
        {
            if (string.IsNullOrWhiteSpace(address) || filter == null)
            {
                return BadRequest("Invalid parameter");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                IEnumerable<PhotoDTO> photoDTOs = await photoService.GetSavedPhotosAsync(p => p.Address.Contains(address));
                PagedResponse<IEnumerable<Models.Photo.Photo>> pagedResponse = ApplyFilter(filter, photoDTOs);

                return Ok(pagedResponse);
            }
            catch (ArgumentException ex)
            {
                PagedResponse<IEnumerable<Models.Photo.Photo>> reponse = new PagedResponse<IEnumerable<Models.Photo.Photo>>(null, 0, 0)
                {
                    Succeeded = false,
                    Error = ex.Message
                };

                return Content(System.Net.HttpStatusCode.BadRequest, reponse);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [JwtAuthentication]
        [HttpGet]
        [ResponseType(typeof(Response<IEnumerable<Models.Photo.Photo>>))]
        [Route("search/address")]
        public async Task<IHttpActionResult> SearchByAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return BadRequest("Invalid parameter");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                IEnumerable<PhotoDTO> photoDTOs = await photoService.SearchPhotosAsync(address);
                return Ok(new Response<IEnumerable<Models.Photo.Photo>>(photoDTOs.Select(p => p.ToPhoto()).ToList()));
            }
            catch (ArgumentException ex)
            {
                Response<IEnumerable<Models.Photo.Photo>> reponse = new Response<IEnumerable<Models.Photo.Photo>>(null)
                {
                    Succeeded = false,
                    Error = ex.Message
                };

                return Content(System.Net.HttpStatusCode.BadRequest, reponse);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [JwtAuthentication]
        [HttpGet]
        [ResponseType(typeof(Response<IEnumerable<Models.Photo.Photo>>))]
        [Route("search")]
        public async Task<IHttpActionResult> SearchByGeoCodes([FromUri]Models.Photo.GeoCodes geoCodes)
        {
            try
            {
                GeoCodesDTO geoCodesDTO = new GeoCodesDTO
                {
                    Latitude = geoCodes.Latitude,
                    Longitude = geoCodes.Longitude
                };

                IEnumerable<PhotoDTO> photoDTOs = await photoService.SearchPhotosAsync(geoCodesDTO);
                return Ok(new Response<IEnumerable<Models.Photo.Photo>>(photoDTOs.Select(p => p.ToPhoto()).ToList()));
            }
            catch (ArgumentException ex)
            {
                Response<IEnumerable<Models.Photo.Photo>> reponse = new Response<IEnumerable<Models.Photo.Photo>>(null)
                {
                    Succeeded = false,
                    Error = ex.Message
                };

                return Content(System.Net.HttpStatusCode.BadRequest, reponse);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [JwtAuthentication]
        [HttpPost]
        [ResponseType(typeof(Response<IEnumerable<Models.Photo.Photo>>))]
        [Route("from-address")]
        public async Task<IHttpActionResult> PostPhotos([FromBody]Models.Photo.FromAddress fromAddress)
        {
            if (fromAddress == null || string.IsNullOrWhiteSpace(fromAddress.Address))
            {
                return BadRequest("Invalid passed data");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                IEnumerable<PhotoDTO> photoDTOs = await photoService.InsertPhotosAsync(fromAddress.Address);
                return Ok(new Response<IEnumerable<Models.Photo.Photo>>(photoDTOs.Select(p => p.ToPhoto()).ToList()));
            }
            catch (ArgumentException ex)
            {
                Response<IEnumerable<Models.Photo.Photo>> reponse = new Response<IEnumerable<Models.Photo.Photo>>(null)
                {
                    Succeeded = false,
                    Error = ex.Message
                };

                return Content(System.Net.HttpStatusCode.BadRequest, reponse);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [JwtAuthentication]
        [HttpPost]
        [ResponseType(typeof(Response<IEnumerable<Models.Photo.Photo>>))]
        [Route("from-geocode")]
        public async Task<IHttpActionResult> PostPhotos([FromBody]Models.Photo.GeoCodes geoCodesInsert)
        {
            if (geoCodesInsert == null)
            {
                return BadRequest("Invalid passed data");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                GeoCodesDTO geoCodes = new GeoCodesDTO
                {
                    Latitude = geoCodesInsert.Latitude,
                    Longitude = geoCodesInsert.Longitude
                };

                IEnumerable<PhotoDTO> photoDTOs = await photoService.InsertPhotosAsync(geoCodes);
                return Ok(new Response<IEnumerable<Models.Photo.Photo>>(photoDTOs.Select(p => p.ToPhoto()).ToList()));
            }
            catch (ArgumentException ex)
            {
                Response<IEnumerable<Models.Photo.Photo>> reponse = new Response<IEnumerable<Models.Photo.Photo>>(null)
                {
                    Succeeded = false,
                    Error = ex.Message
                };

                return Content(System.Net.HttpStatusCode.BadRequest, reponse);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        private PagedResponse<IEnumerable<Models.Photo.Photo>> ApplyFilter(PaginationFilter filter, IEnumerable<PhotoDTO> response)
        {
            int totalRecords = response.Count();

            PaginationFilter validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            IEnumerable<PhotoDTO> filtered = response
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize);

            PagedResponse<IEnumerable<Models.Photo.Photo>> pagedResponse = new PagedResponse<IEnumerable<Models.Photo.Photo>>(filtered.Select(p => p.ToPhoto()).ToList(),
                validFilter.PageNumber,
                validFilter.PageSize)
            {
                TotalRecords = totalRecords,
                TotalPages = validFilter.PageSize == 0 ? 0 : (int)Math.Ceiling((decimal)(totalRecords / validFilter.PageSize)),
            };

            return pagedResponse;
        }
    }
}