using Microsoft.AspNetCore.Mvc;
using UserRegistration.API.DTOS;
using UserRegistration.BLL.Interfaces;

namespace UserRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        string role = "user";
        private readonly IUserManagerService _userManagerService;
        private readonly IJwtService _jwtService;

        public AccountController(IUserManagerService userManagerService, IJwtService jwtService)
        {
            _userManagerService = userManagerService;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public IActionResult Login(SignInDTO dto)
        {
            var logiginSuccess = _userManagerService.TryLogin(dto.UserName, dto.Password, out role, out Guid? userId);
            if (!logiginSuccess)
            {
                return BadRequest("Wrong username or password");
            }
            var token = _jwtService.GetJwtToken((Guid)userId, dto.UserName, role);

            return Ok(token);
        }

        [HttpPost("SignUp")]
        public IActionResult Register(SignUpDTO dto)
        {
            var user = _userManagerService.CreateAccount(dto.UserName, dto.Password, dto.Email);
            if (user == null)
            {
                return BadRequest("User already exists ");
            }
            return Ok();
        }
    }
}
