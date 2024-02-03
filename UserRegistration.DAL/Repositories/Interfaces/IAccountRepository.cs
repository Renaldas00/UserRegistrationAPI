using UserRegistration.DAL.Entities;

namespace UserRegistration.DAL.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Insert Account Model Into Database
        /// </summary>
        /// <param name="model">Account Model To Insert</param>
        /// <returns>Account GUID/UUID</returns>
        Guid Create(Account model);
        /// <summary>
        /// Delete Account From Database (By Admin Only)
        /// </summary>
        /// <param name="id">Account GUID To Delete</param>
        void Delete(Guid id);
        /// <summary>
        ///  Retrieve Account From Database
        /// </summary>
        /// <param name="id">GUID To Retrieve By</param>
        /// <returns></returns>
        bool Exists(Guid id);
        /// <summary>
        /// Check If Account Exists
        /// </summary>
        /// <param name="userName">Username To Search For</param>
        /// <returns></returns>
        Account? Get(string userName);
    }
}
