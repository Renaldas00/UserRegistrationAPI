using UserRegistration.API.DTOS.Requests;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers.Interfaces
{
    public interface IAccountMapper
    {
        /// <summary>
        /// Maps sign-up data from a SignUpRequestDTO to an Account entity.
        /// </summary>
        /// <param name="dto">The sign-up data provided in a SignUpRequestDTO.</param>
        /// <returns>The Account entity mapped from the sign-up data.</returns>
        Account Map(SignUpRequestDTO dto);

    }
}
