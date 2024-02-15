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
    public class UserDataController : ControllerBase
    {
        // Intercace instances
        private readonly ILogger<UserDataController> _logger;
        private readonly IUserDataRepository _repository;
        private readonly IUserDataMapper _mapper;
        private readonly Guid _userId;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor with dependency injection to initialize controller properties.
        public UserDataController(ILogger<UserDataController> logger,
            IUserDataRepository repository,
            IHttpContextAccessor httpContextAccessor,
            IUserDataMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _userId = new Guid(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Get User Data Id Based On UUID
        /// </summary>
        /// <response code="200">User Data Id</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpGet]
        [ProducesResponseType(typeof(UserDataListResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetAll()
        {
            // Log information about retrieving user data for the current user.
            _logger.LogInformation($"Getting user data for user {_userId}");
            // Retrieve user data for the current user from the repository.
            var entity = _repository.GetAll(userData => userData.Image, userData => userData.Location)
                 .Where(userData => userData.AccountId == _userId)
                 .ToList();

            if (entity == null)
            {
                _logger.LogInformation($"User data for user {_userId} not found");
                return NotFound();
            }
            // Map the retrieved user data to DTOs.
            var dto = _mapper.Map(entity);
            // Return the mapped DTOs in a 200 OK response.
            return Ok(dto);
        }

        /// <summary>
        /// Get User Data For User
        /// </summary>
        /// <param name="id">User Data ID</param>
        /// <response code="200">User Data</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDataListResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Get(int id)
        {
            // Log information about retrieving user data for the current user by item id.
            _logger.LogInformation($"Getting user data with id {id} for user {_userId}");
            // Retrieve user data for the current user from the repository.
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"User data item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            // Map the retrieved user data to a DTO.
            var dto = _mapper.Map(entity);
            // Return the mapped DTO in a 200 OK response.
            return Ok(dto);
        }

        /// <summary>
        /// Creates User Data Item
        /// </summary>
        /// <param name="req">User Data To Create</param>
        /// <response code="201">User ID</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Post(CreateUserDataRequestDTO req)
        {
            _logger.LogInformation($"Creating user data for user {_userId} with Title {req.FirstName}");
            // Map the request data to a UserData entity.
            var entity = _mapper.Map(req);
            // Add the mapped entity to the repository.
            _repository.Add(entity);
            // Return the created entity in a 201 Created response.
            return Ok(entity);
        }

        /// <summary>
        /// Updates User Data First Name
        /// </summary>
        /// <param name="id">User Data ID</param>
        /// <param name="req">User Data Item FirstName</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpPut("firstname/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateFirstNameRequestDTO req)
        {
            _logger.LogInformation($"Updating user data item with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"User data item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            // Update the first name of the user data item based on the request data.
            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// Updates User Data Last Name
        /// </summary>
        /// <param name="id">User Data ID</param>
        /// <param name="req">User Data Item Last Name</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpPut("lastname/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateLastNameRequestDTO req)
        {
            _logger.LogInformation($"Updating user data item with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"User data item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// Updates User Data Email
        /// </summary>
        /// <param name="id">User Data ID</param>
        /// <param name="req">User Data Item Email</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpPut("emailAdress/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateEmailAddresRequestDTO req)
        {
            _logger.LogInformation($"Updating user data item with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"User data item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// Updates User Data Social Security Code
        /// </summary>
        /// <param name="id">User Data ID</param>
        /// <param name="req">User Data Item Social Security Code</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpPut("socialsecuritycode/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateSocSecCodeRequestDTO req)
        {
            _logger.LogInformation($"Updating user data item with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"User data item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// Updates User Data Phone Number
        /// </summary>
        /// <param name="id">User Data ID</param>
        /// <param name="req">User Data Item Phone Number</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpPut("phonenumber/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdatePhoneNumberRequestDTO req)
        {
            _logger.LogInformation($"Updating user data item with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"User data item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// Deletes User Data
        /// </summary>
        /// <param name="id">UserData ID</param>
        /// <response code="204">No Content</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Deleting userdata with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Userdata with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"Userdata with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            _repository.Delete(entity);
            return NoContent();
        }
    }
}
