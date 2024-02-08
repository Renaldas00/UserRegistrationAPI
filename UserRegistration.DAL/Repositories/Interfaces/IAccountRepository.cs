using UserRegistration.DAL.Entities;

namespace UserRegistration.DAL.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Inserts an Account model into the database.
        /// </summary>
        /// <param name="model">The Account model to insert.</param>
        /// <returns>The GUID/UUID of the inserted Account.</returns>
        Guid Create(Account model);

        /// <summary>
        /// Deletes an Account from the database (By Admin Only).
        /// </summary>
        /// <param name="id">The GUID of the Account to delete.</param>
        void Delete(Guid id);

        /// <summary>
        /// Retrieves an Account from the database.
        /// </summary>
        /// <param name="id">The GUID to retrieve the Account by.</param>
        /// <returns>True if the Account exists; otherwise, false.</returns>
        bool Exists(Guid id);

        /// <summary>
        /// Checks if an Account exists.
        /// </summary>
        /// <param name="userName">The username to search for.</param>
        /// <returns>The Account if found; otherwise, null.</returns>
        Account? Get(string userName);
    }
}
