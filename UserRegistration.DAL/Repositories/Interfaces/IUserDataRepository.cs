using UserRegistration.DAL.Entities;

namespace UserRegistration.DAL.Repositories.Interfaces
{
    public interface IUserDataRepository
    {
        /// <summary>
        /// Inserts User Data List Into Database
        /// </summary>
        /// <param name="userData">User Data List That Needs To Be Inserted</param>
        /// <returns>User Data List ID</returns>
        int Create(UserData userData);
        /// <summary>
        /// Retrieves User Data List From Database
        /// </summary>
        /// <param name="id">User Data List ID</param>
        /// <returns>User Data List From Database</returns>
        UserData Get(int id);
        /// <summary>
        /// Updates User Data List In Database
        /// </summary>
        /// <param name="userData">User Data List That Needs To Be Updated</param>
        void Update(UserData userData);
        /// <summary>
        /// Deletes User Data List And All Of Its Associated Items Thru The Foreign Key From The Database
        /// </summary>
        /// <param name="id">User Data List ID</param>
        void Delete(UserData userData);
    }
}
