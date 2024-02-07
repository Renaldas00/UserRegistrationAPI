using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.DTOS.Responses;
using UserRegistration.API.Mappers.Interfaces;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly ILocationRepository _repository;
        private readonly ILocationMapper _mapper;
        private readonly Guid _userId;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocationController(ILogger<LocationController> logger,
            ILocationRepository repository,
            IHttpContextAccessor httpContextAccessor,
            ILocationMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _userId = new Guid(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets user data list item for the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LocationResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Getting user data with id {id} for user {_userId}");
            var entity = _repository.GetLocationListById(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data with id {id} for user {_userId} not found");
                return NotFound();
            }
            var dto = _mapper.Map(entity);
            return Ok(dto);
        }

        /// <summary>
        /// Creates User Data Item
        /// </summary>
        /// <param name="req">User Data To Create</param>
        /// <returns>Created User Data ID</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Post(LocationItemRequestDTO req, int id)
        {
            _logger.LogInformation($"Creating user data list for user {_userId} with Title {req.Country}");
            var entity = _mapper.Map(req, id);
            _repository.CreateLocationList(entity);

            return Ok(entity);
        }

        /// <summary>
        /// Updates a user data list item first name for the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("country/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateCountryRequestDTO req)
        {
            _logger.LogInformation($"Updating user data list item with id {id} for user {_userId}");
            var entity = _repository.GetLocationListById(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserLocation.AccountId != _userId)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _repository.UpdateLocationList(entity);
            return NoContent();
        }
        /// <summary>
        /// Updates a user data list item last name for the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("city/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateCityRequestDTO req)
        {
            _logger.LogInformation($"Updating user data list item with id {id} for user {_userId}");
            var entity = _repository.GetLocationListById(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserLocation.AccountId != _userId)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _repository.UpdateLocationList(entity);
            return NoContent();
        }
        /// <summary>
        /// Updates a user data list item last name for the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("street/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateStreetRequestDTO req)
        {
            _logger.LogInformation($"Updating user data list item with id {id} for user {_userId}");
            var entity = _repository.GetLocationListById(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserLocation.AccountId != _userId)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _repository.UpdateLocationList(entity);
            return NoContent();
        }
        /// <summary>
        /// Updates a user data list item last name for the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("housenumber/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateHouseNumberRequestDTO req)
        {
            _logger.LogInformation($"Updating user data list item with id {id} for user {_userId}");
            var entity = _repository.GetLocationListById(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserLocation.AccountId != _userId)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _repository.UpdateLocationList(entity);
            return NoContent();
        }
        /// <summary>
        /// Updates a user data list item last name for the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("apartmentnumber/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateApartmentNumberRequestDTO req)
        {
            _logger.LogInformation($"Updating user data list item with id {id} for user {_userId}");
            var entity = _repository.GetLocationListById(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserLocation.AccountId != _userId)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _repository.UpdateLocationList(entity);
            return NoContent();
        }

        /// <summary>
        /// Deletes user data list item for the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Deleting todo with id {id} for user {_userId}");
            var entity = _repository.GetLocationListById(id);
            if (entity == null)
            {
                _logger.LogInformation($"Todo with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserLocation.AccountId != _userId)
            {
                _logger.LogInformation($"Todo with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            _repository.DeleteLocationList(entity);
            return NoContent();
        }
    }
}
