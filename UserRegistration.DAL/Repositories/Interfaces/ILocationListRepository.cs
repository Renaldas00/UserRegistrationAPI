using UserRegistration.DAL.Entities;

namespace UserRegistration.DAL.Repositories.Interfaces
{
    public interface ILocationListRepository
    {
        /// <summary>
        /// Inserts Location List Into Database
        /// </summary>
        /// <param name="LocationList">Location List To Insert</param>
        /// <returns>Inserted Location List ID</returns>
        int CreateLocationList(LocationList LocationList);
        /// <summary>
        /// Retrieves Location List By Id From Database
        /// </summary>
        /// <param name="id">Location List ID To Retrieve</param>
        /// <returns>Location List</returns>
        LocationList GetLocationList(int id);
        /// <summary>
        /// Retrieves Location List Based On User ID From Database
        /// </summary>
        /// <param name="UserId">UUID To Base Search Upon</param>
        /// <returns>Location List</returns>
        List<LocationList> GetLocationListByUserId(int UserId);
        /// <summary>
        /// Updates Location List In Database
        /// </summary>
        /// <param name="LocationList">Location List To Update And New Content</param>
        void UpdateLocationList(LocationList LocationList);
        /// <summary>
        /// Deletes Location List From Database
        /// </summary>
        /// <param name="Id">ID To Base Deletion Upon</param>
        void DeleteLocationList(int Id);
    }
}
