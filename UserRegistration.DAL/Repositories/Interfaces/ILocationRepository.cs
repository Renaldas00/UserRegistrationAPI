using UserRegistration.DAL.Entities;

namespace UserRegistration.DAL.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        /// <summary>
        /// Inserts Location List Into Database
        /// </summary>
        /// <param name="locationList">Location List To Insert</param>
        /// <returns>Inserted Location List ID</returns>
        int CreateLocationList(Location locationList);
        /// <summary>
        /// Retrieves Location List Based On User ID From Database
        /// </summary>
        /// <param name="id">UUID To Base Search Upon</param>
        /// <returns>Location List</returns>
        Location GetLocationListById(int id);
        /// <summary>
        /// Updates Location List In Database
        /// </summary>
        /// <param name="locationList">Location List To Update And New Content</param>
        void UpdateLocationList(Location locationList);
        /// <summary>
        /// Deletes Location List From Database
        /// </summary>
        /// <param name="locationList">ID To Base Deletion Upon</param>
        void DeleteLocationList(Location locationList);
    }
}
