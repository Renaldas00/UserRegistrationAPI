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
        /// Get Location Data For User
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <response code="200">Location Data</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LocationResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Getting location data {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Location data {id} for user {_userId} not found");
                return NotFound();
            }
            var dto = _mapper.Map(entity);
            return Ok(dto);
        }

        /// <summary>
        /// Create Location Data Item
        /// </summary>
        /// <param name="req">Location Data To Create</param>
        /// <response code="201">Location Data</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">System error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Post(CreateLocationItemRequestDTO req, int id)
        {
            _logger.LogInformation($"Creating location data item for user {_userId} with Title {req.Country}");
            var entity = _mapper.Map(req, id);
            _repository.Add(entity);

            return Ok(entity);
        }

        /// <summary>
        /// Update Location Item Country For User
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <param name="req">Location Data Country</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpPut("country/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateCountryRequestDTO req)
        {
            _logger.LogInformation($"Updating location item country with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Location item country with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserLocation.AccountId != _userId)
            {
                _logger.LogInformation($"Location item country with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// Update Location Item City For User
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <param name="req">Location Data City</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpPut("city/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateCityRequestDTO req)
        {
            _logger.LogInformation($"Updating Location item city with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Location item city with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserLocation.AccountId != _userId)
            {
                _logger.LogInformation($"Location item city with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// Update Location Item Street For User
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <param name="req">Location Data Street</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpPut("street/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateStreetRequestDTO req)
        {
            _logger.LogInformation($"Updating location street item with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Location street item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserLocation.AccountId != _userId)
            {
                _logger.LogInformation($"Location street item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// Update Location Item House Number For User
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <param name="req">Location Data House Number</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpPut("housenumber/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateHouseNumberRequestDTO req)
        {
            _logger.LogInformation($"Updating location house number item {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Location house number item {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserLocation.AccountId != _userId)
            {
                _logger.LogInformation($"Location house number item {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// Update Location Item Apartment Number For User
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <param name="req">Location Data Apartment Number</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpPut("apartmentnumber/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateApartmentNumberRequestDTO req)
        {
            _logger.LogInformation($"Updating location apartment number item with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Location apartment number item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserLocation.AccountId != _userId)
            {
                _logger.LogInformation($"Location apartment number item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// Deletes Location
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <response code="204">No Content</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Deleting location with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Location with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserLocation.AccountId != _userId)
            {
                _logger.LogInformation($"Location with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            _repository.Delete(entity);
            return NoContent();
        }
    }
}
