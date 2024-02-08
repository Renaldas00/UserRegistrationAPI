using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.DTOS.Responses;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers.Interfaces
{
    public interface ILocationMapper
    {
        /// <summary>
        /// Maps a Location entity to a LocationResultDTO.
        /// </summary>
        /// <param name="entity">The Location entity to map.</param>
        /// <returns>The LocationResultDTO mapped from the Location entity.</returns>
        LocationResultDTO Map(Location entity);

        /// <summary>
        /// Maps a collection of Location entities to a collection of LocationResultDTOs.
        /// </summary>
        /// <param name="entities">The collection of Location entities to map.</param>
        /// <returns>A collection of LocationResultDTOs mapped from the Location entities.</returns>
        List<LocationResultDTO> Map(IEnumerable<Location> entities);

        /// <summary>
        /// Maps a CreateLocationItemRequestDTO to a Location entity with the specified ID.
        /// </summary>
        /// <param name="dto">The CreateLocationItemRequestDTO containing the location data.</param>
        /// <param name="id">The ID of the location.</param>
        /// <returns>The Location entity mapped from the CreateLocationItemRequestDTO.</returns>
        Location Map(CreateLocationItemRequestDTO dto, int id);

        /// <summary>
        /// Projects properties from an UpdateCountryRequestDTO to an existing Location entity.
        /// </summary>
        /// <param name="from">The UpdateCountryRequestDTO containing the updated country data.</param>
        /// <param name="to">The existing Location entity to update.</param>
        void ProjectTo(UpdateCountryRequestDTO from, Location to);

        /// <summary>
        /// Projects properties from an UpdateCityRequestDTO to an existing Location entity.
        /// </summary>
        /// <param name="from">The UpdateCityRequestDTO containing the updated city data.</param>
        /// <param name="to">The existing Location entity to update.</param>
        void ProjectTo(UpdateCityRequestDTO from, Location to);

        /// <summary>
        /// Projects properties from an UpdateStreetRequestDTO to an existing Location entity.
        /// </summary>
        /// <param name="from">The UpdateStreetRequestDTO containing the updated street data.</param>
        /// <param name="to">The existing Location entity to update.</param>
        void ProjectTo(UpdateStreetRequestDTO from, Location to);

        /// <summary>
        /// Projects properties from an UpdateHouseNumberRequestDTO to an existing Location entity.
        /// </summary>
        /// <param name="from">The UpdateHouseNumberRequestDTO containing the updated house number data.</param>
        /// <param name="to">The existing Location entity to update.</param>
        void ProjectTo(UpdateHouseNumberRequestDTO from, Location to);

        /// <summary>
        /// Projects properties from an UpdateApartmentNumberRequestDTO to an existing Location entity.
        /// </summary>
        /// <param name="from">The UpdateApartmentNumberRequestDTO containing the updated apartment number data.</param>
        /// <param name="to">The existing Location entity to update.</param>
        void ProjectTo(UpdateApartmentNumberRequestDTO from, Location to);

    }
}
