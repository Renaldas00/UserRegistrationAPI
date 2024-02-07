using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.DTOS.Responses;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers.Interfaces
{
    public interface ILocationMapper
    {
        LocationResultDTO Map(Location entity);
        List<LocationResultDTO> Map(IEnumerable<Location> entities);
        Location Map(LocationItemRequestDTO dto, int id);

        void ProjectTo(UpdateCountryRequestDTO from, Location to);

        void ProjectTo(UpdateCityRequestDTO from, Location to);

        void ProjectTo(UpdateStreetRequestDTO from, Location to);

        void ProjectTo(UpdateHouseNumberRequestDTO from, Location to);

        void ProjectTo(UpdateApartmentNumberRequestDTO from, Location to);
    }
}
