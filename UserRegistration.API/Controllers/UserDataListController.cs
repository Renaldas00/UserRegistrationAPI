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
        /// Gets user data list item for the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDataListResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        public IActionResult Post(UserDataRequestDTO req)
        {
            _logger.LogInformation($"Creating user data list for user {_userId} with Title {req.FirstName}");
            var entity = _mapper.Map(req);
            _repository.Add(entity);

            return Ok(entity);
        }

        /// <summary>
        /// Updates a user data list item first name for the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("firstname/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateFirstNameRequestDTO req)
        {
            _logger.LogInformation($"Updating user data list item with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }
        /// <summary>
        /// Updates a user data list item last name for the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("lastname/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateLastNameRequestDTO req)
        {
            _logger.LogInformation($"Updating user data list item with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }
        /// <summary>
        /// Updates a user data list item last name for the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("emailaddress/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateEmailAddresRequestDTO req)
        {
            _logger.LogInformation($"Updating user data list item with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }
        /// <summary>
        /// Updates a user data list item last name for the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("socialsecuritycode/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdateSocSecCodeRequestDTO req)
        {
            _logger.LogInformation($"Updating user data list item with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
            return NoContent();
        }
        /// <summary>
        /// Updates a user data list item last name for the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("phonenumber/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, UpdatePhoneNumberRequestDTO req)
        {
            _logger.LogInformation($"Updating user data list item with id {id} for user {_userId}");
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _repository.Update(entity);
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
            var entity = _repository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Todo with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.AccountId != _userId)
            {
                _logger.LogInformation($"Todo with id {id} for user {_userId} is forbidden");
                return Forbid();
            }
            _repository.Delete(entity);
            return NoContent();
        }
    }
}
