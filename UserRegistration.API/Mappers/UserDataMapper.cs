using System.Security.Claims;
using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.DTOS.Responses;
using UserRegistration.API.Mappers.Interfaces;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers
{
    public class UserDataMapper : IUserDataMapper
    {
        private readonly Guid accountId;
        public UserDataMapper(IHttpContextAccessor httpContextAccessor)
        {
            accountId = new Guid(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        public UserDataListResultDTO Map(UserData entity)
        {
            var result = new UserDataListResultDTO
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                SocialSecurityCode = entity.SocialSecurityCode,
                EmailAddres = entity.EmailAddres,
                PhoneNumber = entity.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
            };

            if (entity.Image != null)
            {
                result.Image = entity.Image.Content;
            }

            if (entity.Location != null)
            {
                result.Country = entity.Location.Country;
                result.City = entity.Location.City;
                result.Street = entity.Location.Street;
                result.HouseNumber = entity.Location.HouseNumber;
                result.ApartmentNumber = entity.Location.ApartmentNumber;

            }

            return result;
        }

        public List<UserDataListResultDTO> Map(IEnumerable<UserData> entities)
        {
            return entities.Select(x => Map(x)).ToList();
        }

        public UserData Map(UserDataListRequestDTO dto)
        {
            return new UserData
            {

                FirstName = dto.FirstName,
                LastName = dto.LastName,
                SocialSecurityCode = dto.SocialSecurityCode,
                EmailAddres = dto.EmailAddres,
                PhoneNumber = dto.PhoneNumber,
                AccountId = accountId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public void ProjectTo(UpdateFirstNameRequestDTO from, UserData to)
        {
            to.FirstName = from.FirstName!;
            to.UpdatedAt = DateTime.UtcNow;
        }

        public void ProjectTo(UpdateLastNameRequestDTO from, UserData to)
        {
            to.LastName = from.LastName!;
            to.UpdatedAt = DateTime.UtcNow;
        }

        public void ProjectTo(UpdateEmailAddresRequestDTO from, UserData to)
        {
            to.EmailAddres = from.EmailAdress!;
            to.UpdatedAt = DateTime.UtcNow;
        }

        public void ProjectTo(UpdateSocSecCodeRequestDTO from, UserData to)
        {
            to.SocialSecurityCode = from.SocialSecurityCode!;
            to.UpdatedAt = DateTime.UtcNow;
        }

        public void ProjectTo(UpdatePhoneNumberRequestDTO from, UserData to)
        {
            to.PhoneNumber = from.PhoneNumber!;
            to.UpdatedAt = DateTime.UtcNow;
        }
    }
}
