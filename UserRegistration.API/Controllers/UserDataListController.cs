using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.Mappers.Interfaces;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class UserDataController : ControllerBase
    {
        private readonly ILogger<UserDataController> _logger;
        private readonly IUserDataListRepository _repository;
        private readonly IUserDataListMapper _mapper;
        private readonly Guid _userId;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserDataController(ILogger<UserDataController> logger,
            IUserDataListRepository repository,
            IHttpContextAccessor httpContextAccessor,
            IUserDataListMapper mapper, Guid userId)
        {
            _logger = logger;
            _repository = repository;
            _userId = new Guid(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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
        public IActionResult Post(UserDataListRequestDTO req)
        {
            _logger.LogInformation($"Creating todo for user {_userId} with Title {req.FirstName}");
            var entity = _mapper.Map(req);
            _repository.CreateUserDataList(entity);

            return Ok(entity);
            //return Created(nameof(Get), new { id = entity.Id });
        }
    }
}
