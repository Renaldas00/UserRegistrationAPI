using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.Mappers.Interfaces;
using UserRegistration.BLL.Interfaces;
using UserRegistration.BLL.Services.Interfaces;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountRepository _repository;
        private readonly IJwtService _jwtService;
        private readonly IAccountMapper _mapper;
        private readonly IAccountService _service;

        public AccountsController(ILogger<AccountsController> logger,
            IAccountRepository repository,
            IJwtService jwtService,
            IAccountMapper mapper,
            IAccountService service)
        {
            _logger = logger;
            _repository = repository;
            _jwtService = jwtService;
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        ///  User Sign Up
        /// </summary>
        /// <param name="req">User Account Details</param>
        /// <response code="201">UUID</response>
        /// <response code="400">Model validation error</response>
        /// <response code="500">System error</response>
        [HttpPost("signup")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult SignUp(SignUpRequestDTO req)
        {
            _logger.LogInformation($"Creating account for {req.UserName}");
            var account = _mapper.Map(req);
            var userId = _repository.Create(account);
            _logger.LogInformation($"Account for {req.UserName} created with id {userId}");
            return Created("", new { id = userId });
        }


        /// <summary>
        ///  User Sign In
        /// </summary>
        /// <param name="req">User Account Details</param>
        /// <response code="200">User JWT</response>
        /// <response code="400">Model validation error</response>
        /// <response code="500">System error</response>
        [HttpPost("signin")]
        [Produces(MediaTypeNames.Text.Plain)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Login(SignInDTO req)
        {
            _logger.LogInformation($"Login attempt for {req.UserName}");
            var account = _repository.Get(req.UserName!);
            if (account == null)
            {
                _logger.LogWarning($"User {req.UserName} not found");
                return BadRequest("User nor found");
            }

            var isPasswordValid = _service.VerifyPasswordHash(req.Password, account.PasswordHash, account.PasswordSalt);

            if (!isPasswordValid)
            {
                _logger.LogWarning($"Invalid password for {req.UserName}");
                return BadRequest("Invalid username or password");
            }
            _logger.LogInformation($"User {req.UserName} successfully logged in");
            var jwt = _jwtService.GetJwtToken(account);
            return Ok(jwt);

        }

        /// <summary>
        /// User Account Removal By Admin Only
        /// </summary>
        /// <param name="id">User Account ID To Be Removed</param>
        /// <returns>No Content</returns>
        /// <response code="204">User Remove Successfully</response>
        /// <response code="404">Not Found error</response>
        /// <response code="500">System error</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(Guid id)
        {
            _logger.LogInformation($"Deleting account {id}");
            if (!_repository.Exists(id))
            {
                _logger.LogInformation($"Account {id} not found");
                return NotFound();
            }
            _repository.Delete(id);
            return NoContent();
        }
    }
}
