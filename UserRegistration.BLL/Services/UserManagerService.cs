using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserRegistration.BLL.Interfaces;
using UserRegistration.DAL;
using UserRegistration.DAL.Entities;

namespace UserRegistration.BLL.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<UserManagerService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManagerService(AppDbContext appDbContext, ILogger<UserManagerService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public User? CreateAccount(string userName, string password, string email)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            var found = _appDbContext.Users.Any(u => u.UserName == userName);
            if (found)
            {
                throw new System.Exception("User already exists");
            }
            var user = new User
            {
                UserName = userName,
                Email = email,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "user"
            };
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            return user;

        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return int.Parse(userId);
        }

        public bool TryLogin(string userName, string password, out string role, out Guid? userId)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                _logger.LogWarning($"User {userName} does not exist");
                role = "";
                userId = null;
                return false;
            }
            role = user.Role;
            userId = user.Id;
            var verified = TryVerifyPasswordHash(password, user.Password, user.PasswordSalt);
            if (!verified)
            {
                _logger.LogWarning($"User {userName} password does not match");
            }
            return verified;


        }

        public bool TryVerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA256(storedSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
    }
}
