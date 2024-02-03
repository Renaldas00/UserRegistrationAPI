using UserRegistration.DAL.Entities;

namespace UserRegistration.DAL.Repositories.Interfaces
{
    public interface IPersonalDataListRepository
    {
        /// <summary>
        /// Inserts Personal Data List Into Database
        /// </summary>
        /// <param name="PersonalDataList">Personal Data List That Needs To Be Inserted</param>
        /// <returns>Personal Data List ID</returns>
        int CreatePersonalDataList(UserDataList PersonalDataList);
        /// <summary>
        /// Retrieves Personal Data List From Database
        /// </summary>
        /// <param name="Id">Personal Data List ID</param>
        /// <returns>Personal Data List From Database</returns>
        UserDataList GetPersonalDataList(int Id);
        /// <summary>
        /// Retrieves Personal Data List By UUID
        /// </summary>
        /// <param name="UserId">User UUID To Retrieve</param>
        /// <returns>Returns List Of Personal Data From Database</returns>
        List<UserDataList> GetPersonalDataListByUserId(Guid UserId);
        /// <summary>
        /// Updates Personal Data List In Database
        /// </summary>
        /// <param name="PersonalDataList">Personal Data List That Needs To Be Updated</param>
        void UpdatePersonalDataList(UserDataList PersonalDataList);
        /// <summary>
        /// Deletes Personal Data List And All Of Its Associated Items Thru The Foreign Key From The Database
        /// </summary>
        /// <param name="Id">Personal Data List ID</param>
        void DeletePersonalDataList(int Id);
    }
}
