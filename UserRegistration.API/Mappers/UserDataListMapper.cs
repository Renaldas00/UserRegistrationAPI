using System.Security.Claims;
using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.Mappers.Interfaces;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers
{
    public class UserDataListMapper : IUserDataListMapper
    {
        private readonly Guid accountId;
        public UserDataListMapper(IHttpContextAccessor httpContextAccessor)
        {
            accountId = new Guid(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        public UserDataListRequestDTO Map(UserDataList entity)
        {
            return new UserDataListRequestDTO
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                SocialSecurityCode = entity.SocialSecurityCode,
                EmailAddres = entity.EmailAddres,
                PhoneNumber = entity.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }

        public List<UserDataListRequestDTO> Map(IEnumerable<UserDataList> entities)
        {
            return entities.Select(x => Map(x)).ToList();
        }

        public UserDataList Map(UserDataListRequestDTO dto)
        {
            return new UserDataList
            {

                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                SocialSecurityCode = dto.SocialSecurityCode,
                EmailAddres = dto.EmailAddres,
                PhoneNumber = dto.PhoneNumber,
                AccountId = accountId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }

        public void ProjectTo(UserDataListRequestDTO from, UserDataList to)
        {
            to.FirstName = from.FirstName!;
            to.LastName = from.LastName!;
            to.SocialSecurityCode = from.SocialSecurityCode;
            to.EmailAddres = from.EmailAddres;
            to.PhoneNumber = from.PhoneNumber;
        }
    }
}
