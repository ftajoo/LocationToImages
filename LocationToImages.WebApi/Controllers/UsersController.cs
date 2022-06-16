using LocationToImages.Business.Interfaces;
using LocationToImages.WebApi.Convertors;
using LocationToImages.WebApi.Filters;
using LocationToImages.WebApi.Models.Wrapper;
using LocationToImages.WebApi.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace LocationToImages.WebApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [JwtAuthentication]
        [HttpGet]
        [ResponseType(typeof(Response<Models.User.User>))]
        [Route("{id:int}", Name="GetUserById")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                Business.DTOs.User.UserDTO user = await userService.GetUserAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(new Response<Models.User.User>(user.ToUser()));
            }
            catch (ArgumentException ex)
            {
                Response<Models.User.User> reponse = new Response<Models.User.User>(null)
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

        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(Response<Models.User.Token>))]
        [Route("authenticate")]
        public async Task<IHttpActionResult> Authenticate([FromBody]Models.User.UserAuthenticate userAuthenticate)
        {
            try
            {
                Business.DTOs.User.UserDTO user = await userService.AuthenticateUserAsync(userAuthenticate.ToUserAuthenticateDTO());

                if (user == null)
                {
                    return Unauthorized();
                }

                Models.User.Token token = new Models.User.Token
                {
                    User = user.ToUser(),
                    Expires = 120,
                    JwtToken = JwtManager.GenerateToken(user.Username)
                };

                return Ok(new Response<Models.User.Token>(token));
            }
            catch (ArgumentException ex)
            {
                Response<Models.User.Token> reponse = new Response<Models.User.Token>(null)
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

        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(Response<Models.User.User>))]
        [Route()]
        public async Task<IHttpActionResult> Post([FromBody]Models.User.UserInsert userInsert)
        {
            if (userInsert == null)
            {
                return BadRequest("Invalid passed data");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Business.DTOs.User.UserDTO user = await userService.InsertUserAsync(userInsert.ToUserInsertDTO());
                return CreatedAtRoute("GetUserById", 
                    new { id = user.Id }, 
                    new Response<Models.User.User>(user.ToUser()));
            }
            catch(ArgumentException ex)
            {
                Response<Models.User.User> reponse = new Response<Models.User.User>(null)
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
        [ResponseType(typeof(Response<IEnumerable<Models.Location.GeoLocation>>))]
        [Route("{id:int}/geo-locations")]
        public async Task<IHttpActionResult> GetGeoLocations(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                IEnumerable<Models.Location.GeoLocation> geoLocations = (await userService.GetGeoLocations(id)).Select(g => g.ToGeoLocation());
                return Ok(new Response<IEnumerable<Models.Location.GeoLocation>>(geoLocations.ToList()));
            }
            catch (ArgumentException ex)
            {
                Response<Models.Location.GeoLocation> reponse = new Response<Models.Location.GeoLocation>(null)
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
        [ResponseType(typeof(Response<Models.Location.GeoLocation>))]
        [Route("{id:int}/geo-locations")]
        public async Task<IHttpActionResult> AddGeoLocation(int id, [FromBody]Models.Location.GeoLocation geoLocation)
        {
            if (geoLocation == null)
            {
                return BadRequest("Invalid passed data");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Business.DTOs.Location.GeoLocationDTO geoLocationDTO = await userService.AddGeoLocation(id, geoLocation.ToGeoLocationDTO());
                return CreatedAtRoute("GetUserById",
                    new { id },
                    new Response<Models.Location.GeoLocation>(geoLocationDTO.ToGeoLocation()));
            }
            catch (ArgumentException ex)
            {
                Response<Models.Location.GeoLocation> reponse = new Response<Models.Location.GeoLocation>(null)
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
        [HttpDelete]
        [Route("{id:int}/geo-locations/{geoLocationId:int}")]
        public async Task<IHttpActionResult> DeleteGeoLocation(int id, int geoLocationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await userService.DeleteGeoLocation(id, geoLocationId);
                return Content(System.Net.HttpStatusCode.NoContent, new Response<int>(geoLocationId));
            }
            catch (ArgumentException ex)
            {
                Response<Models.Location.GeoLocation> reponse = new Response<Models.Location.GeoLocation>(null)
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
    }
}