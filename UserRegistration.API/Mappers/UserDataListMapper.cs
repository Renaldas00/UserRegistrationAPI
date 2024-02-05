using System.Security.Claims;
using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.DTOS.Responses;
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

        public UserDataListResultDTO Map(UserDataList entity)
        {
            return new UserDataListResultDTO
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                SocialSecurityCode = entity.SocialSecurityCode,
                EmailAddres = entity.EmailAddres,
                PhoneNumber = entity.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public List<UserDataListResultDTO> Map(IEnumerable<UserDataList> entities)
        {
            return entities.Select(x => Map(x)).ToList();
        }

        public UserDataList Map(UserDataListRequestDTO dto)
        {
            return new UserDataList
            {

                FirstName = dto.FirstName,
                LastName = dto.LastName,
                SocialSecurityCode = dto.SocialSecurityCode,
                EmailAddres = dto.EmailAddres,
                PhoneNumber = dto.PhoneNumber,
                AccountId = accountId,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public void ProjectTo(UpdateFirstNameUserDataListRequestDTO from, UserDataList to)
        {
            to.FirstName = from.FirstName!;
            to.UpdatedAt = DateTime.UtcNow;
        }

        public void ProjectTo(UpdateLastNameUserDataListRequestDTO from, UserDataList to)
        {
            to.LastName = from.LastName!;
            to.UpdatedAt = DateTime.UtcNow;
        }

        public void ProjectTo(UpdateEmailAddressUserDataListRequestDTO from, UserDataList to)
        {
            to.EmailAddres = from.EmailAdress!;
            to.UpdatedAt = DateTime.UtcNow; ;
        }

        public void ProjectTo(UpdateSocSecCodeUserDataListRequestDTO from, UserDataList to)
        {
            to.SocialSecurityCode = from.SocialSecurityCode!;
            to.UpdatedAt = DateTime.UtcNow;
        }

        public void ProjectTo(UpdatePhoneNumberUserDataListRequestDTO from, UserDataList to)
        {
            to.PhoneNumber = from.PhoneNumber!;
            to.UpdatedAt = DateTime.UtcNow;
        }
    }
}
