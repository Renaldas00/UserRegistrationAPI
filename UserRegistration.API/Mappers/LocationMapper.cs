using System.Security.Claims;
using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.DTOS.Responses;
using UserRegistration.API.Mappers.Interfaces;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers
{
    public class LocationMapper : ILocationMapper
    {
        private readonly Guid accountId;
        public LocationMapper(IHttpContextAccessor httpContextAccessor)
        {
            accountId = new Guid(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
        public LocationResultDTO Map(Location entity)
        {
            return new LocationResultDTO
            {
                Id = entity.Id,
                Country = entity.Country,
                City = entity.City,
                Street = entity.Street,
                HouseNumber = entity.HouseNumber,
                ApartmentNumber = entity.ApartmentNumber,
                CreatedAt = entity.CreatedAt,
            };
        }

        public List<LocationResultDTO> Map(IEnumerable<Location> entities)
        {
            return entities.Select(x => Map(x)).ToList();
        }

        public Location Map(CreateLocationItemRequestDTO dto, int userDataId)
        {
            return new Location
            {
                Country = dto.Country,
                City = dto.City,
                Street = dto.Street,
                HouseNumber = dto.HouseNumber,
                ApartmentNumber = dto.ApartmentNumber,
                CreatedAt = DateTime.UtcNow,
                UserLocationId = userDataId
            };
        }

        public void ProjectTo(UpdateCountryRequestDTO from, Location to)
        {
            to.Country = from.Country;   
        }

        public void ProjectTo(UpdateCityRequestDTO from, Location to)
        {
            to.City = from.City;      
        }

        public void ProjectTo(UpdateStreetRequestDTO from, Location to)
        {
            to.Street = from.Street;         
        }

        public void ProjectTo(UpdateHouseNumberRequestDTO from, Location to)
        {
            to.HouseNumber = from.HouseNumber;        
        }

        public void ProjectTo(UpdateApartmentNumberRequestDTO from, Location to)
        {
            to.ApartmentNumber = from.ApartmentNumber;            
        }
    }
}
