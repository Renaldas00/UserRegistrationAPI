using UserRegistration.API.DTOS.Requests;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers.Interfaces
{
    public interface IAccountMapper
    {
        /// <summary>
        /// Account DTO Data
        /// </summary>
        /// <param name="dto">Sign Up Data</param>
        /// <returns></returns>
        Account Map(SignUpDTO dto);
    }
}
