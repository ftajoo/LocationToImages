using LocationToImages.Business.Interfaces;
using LocationToImages.WebApi.Convertors;
using LocationToImages.WebApi.Filters;
using LocationToImages.WebApi.Models.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace LocationToImages.WebApi.Controllers
{
    [RoutePrefix("api/geo-locations")]
    public class LocationsController : ApiController
    {
        private readonly ILocationService locationService;

        public LocationsController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [JwtAuthentication]
        [HttpGet]
        [ResponseType(typeof(Response<IEnumerable<Models.Location.Location>>))]
        [Route()]
        public async Task<IHttpActionResult> GetLocations()
        {
            try
            {
                IEnumerable<Models.Location.Location> locations = (await locationService.GetLocationsAsync()).Select(l => l.ToLocation());
                return Ok(new Response<IEnumerable<Models.Location.Location>>(locations.ToList()));
            }
            catch (ArgumentException ex)
            {
                Response<IEnumerable<Models.Location.Location>> reponse = new Response<IEnumerable<Models.Location.Location>>(null)
                {
                    Succeeded = false,
                    Error = ex.Message
                };

                return Content(System.Net.HttpStatusCode.Unauthorized, reponse);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [JwtAuthentication]
        [HttpGet]
        [ResponseType(typeof(Response<Models.Location.GeoLocation>))]
        [Route("{address}")]
        public async Task<IHttpActionResult> GetGeoLocation(string address)
        {
            try
            {
                Business.DTOs.Location.GeoLocationDTO geoLocation = await locationService.GetGeoLocationAsync(address);
                return Ok(new Response<Models.Location.GeoLocation>(geoLocation.ToGeoLocation()));
            }
            catch (ArgumentException ex)
            {
                Response<Models.Location.GeoLocation> reponse = new Response<Models.Location.GeoLocation>(null)
                {
                    Succeeded = false,
                    Error = ex.Message
                };

                return Content(System.Net.HttpStatusCode.Unauthorized, reponse);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [JwtAuthentication]
        [HttpPost]
        [ResponseType(typeof(Response<Models.Location.GeoLocation>))]
        [Route("from-address")]
        public async Task<IHttpActionResult> PostGeoLocation([FromBody]Models.Location.FromAddress fromAddress)
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
                Business.DTOs.Location.GeoLocationDTO geoLocation = await locationService.InsertGeoLocationAsync(fromAddress.Address);
                return Ok(new Response<Models.Location.GeoLocation>(geoLocation.ToGeoLocation()));
            }
            catch (ArgumentException ex)
            {
                Response<Models.Location.GeoLocation> reponse = new Response<Models.Location.GeoLocation>(null)
                {
                    Succeeded = false,
                    Error = ex.Message
                };

                return Content(System.Net.HttpStatusCode.Unauthorized, reponse);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [JwtAuthentication]
        [HttpPost]
        [ResponseType(typeof(Response<Models.Location.GeoLocation>))]
        [Route("from-geocodes")]
        public async Task<IHttpActionResult> PostGeoLocation([FromBody]Models.Location.GeoCodes geoCodesInsert)
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
                Business.DTOs.Location.GeoLocationDTO geoLocationDTO = new Business.DTOs.Location.GeoLocationDTO
                {
                    Address = geoCodesInsert.Address,
                    GeoCodes = $"{geoCodesInsert.Latitude},{geoCodesInsert.Longitude}",
                };

                geoLocationDTO = await locationService.InsertGeoLocationAsync(geoLocationDTO);
                return Ok(new Response<Models.Location.GeoLocation>(geoLocationDTO.ToGeoLocation()));
            }
            catch (ArgumentException ex)
            {
                Response<Models.Location.GeoLocation> reponse = new Response<Models.Location.GeoLocation>(null)
                {
                    Succeeded = false,
                    Error = ex.Message
                };

                return Content(System.Net.HttpStatusCode.Unauthorized, reponse);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}