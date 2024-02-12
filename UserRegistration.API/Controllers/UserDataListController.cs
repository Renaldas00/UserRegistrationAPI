using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net.Mime;
using System.Security.Claims;
using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.DTOS.Responses;
using UserRegistration.API.Mappers.Interfaces;
using UserRegistration.DAL.Repositories;
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
        private readonly ILogger<UserDataController> _logger;
        private readonly IUserDataRepository _repository;
        private readonly IUserDataMapper _mapper;
        private readonly Guid _userId;
        private readonly IHttpContextAccessor _httpContextAccessor;

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
            _logger.LogInformation($"Getting user data for user {_userId}");
            //var entity = _repository.GetAll().Where(userData => userData.AccountId == _userId).ToList();
            var entity = _repository.GetAll(userData => userData.Image, userData => userData.Location)
                 .Where(userData => userData.AccountId == _userId)
                 .ToList();
            if (entity == null)
            {
                _logger.LogInformation($"User data for user {_userId} not found");
                return NotFound();
            }
                      
            var dto = _mapper.Map(entity);
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
            _logger.LogInformation($"Getting user data with id {id} for user {_userId}");
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
            var dto = _mapper.Map(entity);
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
            var entity = _mapper.Map(req);
            _repository.Add(entity);

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
