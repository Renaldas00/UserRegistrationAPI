using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.Mappers.Interfaces;
using UserRegistration.BLL.Services.Interfaces;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers
{
    public class AccountMapper : IAccountMapper
    {
        private readonly IAccountService _service;
        public AccountMapper(IAccountService service)
        {
            _service = service;
        }
        public Account Map(SignUpDTO dto)
        {
            _service.CreatePasswordHash(dto.Password!, out var passwordHash, out var passwordSalt);
            return new Account
            {
                UserName = dto.UserName!,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = dto.Role!
            };
        }
    }
}
