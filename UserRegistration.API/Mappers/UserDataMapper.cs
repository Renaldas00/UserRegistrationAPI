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
                Id = entity.Id,
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
                result.ImageListId = entity.Image.Id;
                result.ImageName = entity.Image.ImageName;

            }

            if (entity.Location != null)
            {
                result.Country = entity.Location.Country;
                result.City = entity.Location.City;
                result.Street = entity.Location.Street;
                result.HouseNumber = entity.Location.HouseNumber;
                result.ApartmentNumber = entity.Location.ApartmentNumber;
                result.LocationListId = entity.Location.Id;

            }

            return result;
        }

        public List<UserDataListResultDTO> Map(IEnumerable<UserData> entities)
        {
            return entities.Select(x => Map(x)).ToList();
        }

        public UserData Map(CreateUserDataRequestDTO dto)
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
                
            };
        }

        public void ProjectTo(UpdateFirstNameRequestDTO from, UserData to)
        {
            to.FirstName = from.FirstName!;
            
        }

        public void ProjectTo(UpdateLastNameRequestDTO from, UserData to)
        {
            to.LastName = from.LastName!;
            
        }

        public void ProjectTo(UpdateEmailAddresRequestDTO from, UserData to)
        {
            to.EmailAddres = from.EmailAdress!;
            
        }

        public void ProjectTo(UpdateSocSecCodeRequestDTO from, UserData to)
        {
            to.SocialSecurityCode = from.SocialSecurityCode!;
            
        }

        public void ProjectTo(UpdatePhoneNumberRequestDTO from, UserData to)
        {
            to.PhoneNumber = from.PhoneNumber!;
            
        }
    }
}
