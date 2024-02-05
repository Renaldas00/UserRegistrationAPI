using UserRegistration.DAL.Entities;

namespace UserRegistration.DAL.Repositories.Interfaces
{
    public interface IUserDataListRepository
    {
        /// <summary>
        /// Inserts User Data List Into Database
        /// </summary>
        /// <param name="userDatalist">User Data List That Needs To Be Inserted</param>
        /// <returns>User Data List ID</returns>
        int CreateUserDataList(UserDataList userDatalist);
        /// <summary>
        /// Retrieves User Data List From Database
        /// </summary>
        /// <param name="id">User Data List ID</param>
        /// <returns>User Data List From Database</returns>
        UserDataList GetUserDataList(int id);
        /// <summary>
        /// Retrieves User Data List By UUID
        /// </summary>
        /// <param name="userId">User UUID To Retrieve</param>
        /// <returns>Returns List Of User Data From Database</returns>
        List<UserDataList> GetUserDataListByUserId(Guid userId);
        /// <summary>
        /// Updates User Data List In Database
        /// </summary>
        /// <param name="userDatalist">User Data List That Needs To Be Updated</param>
        void UpdateUserDataList(UserDataList userDatalist);
        /// <summary>
        /// Deletes User Data List And All Of Its Associated Items Thru The Foreign Key From The Database
        /// </summary>
        /// <param name="id">User Data List ID</param>
        void DeleteUserDataList(int id);
    }
}
