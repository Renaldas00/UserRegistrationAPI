using UserRegistration.DAL.Entities;

namespace UserRegistration.BLL.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Account Model Data
        /// </summary>
        /// <param name="account">Acount Model Data</param>
        /// <returns></returns>
        string GetJwtToken(Account account);
    }
}
