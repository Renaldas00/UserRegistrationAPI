using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.DTOS.Responses;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers.Interfaces
{
    public interface IUserDataMapper
    {
        /// <summary>
        /// Maps a UserData entity to a UserDataListResultDTO.
        /// </summary>
        /// <param name="entity">The UserData entity to map.</param>
        /// <returns>The UserDataListResultDTO mapped from the UserData entity.</returns>
        UserDataListResultDTO Map(UserData entity);

        /// <summary>
        /// Maps a collection of UserData entities to a collection of UserDataListResultDTOs.
        /// </summary>
        /// <param name="entities">The collection of UserData entities to map.</param>
        /// <returns>A collection of UserDataListResultDTOs mapped from the UserData entities.</returns>
        List<UserDataListResultDTO> Map(IEnumerable<UserData> entities);

        /// <summary>
        /// Maps a CreateUserDataRequestDTO to a UserData entity.
        /// </summary>
        /// <param name="dto">The CreateUserDataRequestDTO containing the user data.</param>
        /// <returns>The UserData entity mapped from the CreateUserDataRequestDTO.</returns>
        UserData Map(CreateUserDataRequestDTO dto);

        /// <summary>
        /// Projects properties from an UpdateFirstNameRequestDTO to an existing UserData entity.
        /// </summary>
        /// <param name="from">The UpdateFirstNameRequestDTO containing the updated first name data.</param>
        /// <param name="to">The existing UserData entity to update.</param>
        void ProjectTo(UpdateFirstNameRequestDTO from, UserData to);

        /// <summary>
        /// Projects properties from an UpdateLastNameRequestDTO to an existing UserData entity.
        /// </summary>
        /// <param name="from">The UpdateLastNameRequestDTO containing the updated last name data.</param>
        /// <param name="to">The existing UserData entity to update.</param>
        void ProjectTo(UpdateLastNameRequestDTO from, UserData to);

        /// <summary>
        /// Projects properties from an UpdateEmailAddresRequestDTO to an existing UserData entity.
        /// </summary>
        /// <param name="from">The UpdateEmailAddresRequestDTO containing the updated email address data.</param>
        /// <param name="to">The existing UserData entity to update.</param>
        void ProjectTo(UpdateEmailAddresRequestDTO from, UserData to);

        /// <summary>
        /// Projects properties from an UpdateSocSecCodeRequestDTO to an existing UserData entity.
        /// </summary>
        /// <param name="from">The UpdateSocSecCodeRequestDTO containing the updated social security code data.</param>
        /// <param name="to">The existing UserData entity to update.</param>
        void ProjectTo(UpdateSocSecCodeRequestDTO from, UserData to);

        /// <summary>
        /// Projects properties from an UpdatePhoneNumberRequestDTO to an existing UserData entity.
        /// </summary>
        /// <param name="from">The UpdatePhoneNumberRequestDTO containing the updated phone number data.</param>
        /// <param name="to">The existing UserData entity to update.</param>
        void ProjectTo(UpdatePhoneNumberRequestDTO from, UserData to);

    }
}
