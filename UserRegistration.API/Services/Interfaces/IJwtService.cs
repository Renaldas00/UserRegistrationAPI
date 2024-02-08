using UserRegistration.DAL.Entities;

namespace UserRegistration.BLL.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Retrieves a JWT token for the provided Account model data.
        /// </summary>
        /// <param name="account">The Account model data for which to generate a JWT token.</param>
        /// <returns>The JWT token generated for the provided Account model data.</returns>
        string GetJwtToken(Account account);

    }
}
