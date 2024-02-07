using UserRegistration.DAL.Entities;

namespace UserRegistration.DAL.Repositories.Interfaces
{
    public interface IImageRepository
    {
        /// <summary>
        /// Inserts Photo Into Database
        /// </summary>
        /// <param name="photo">Photo Data Which Needs To Be Inserted</param>
        /// <returns>Photo List ID</returns>
        int AddPhoto(Image photo);
        /// <summary>
        /// Updates Photo From Database
        /// </summary>
        /// <param name="photo">Photo Data To Be Updated</param>
        void UpdatePhoto(Image photo);
        /// <summary>
        /// Deletes Photo From Database
        /// </summary>
        /// <param name="Id">Photo ID To Be Deleted</param>
        /// <summary>
        /// Retrieves User Data List From Database
        /// </summary>
        /// <param name="id">User Data List ID</param>
        /// <returns>User Data List From Database</returns>
        Image GetUserPhoto(int id);
        /// <summary>
        /// Deletes User Photo
        /// </summary>
        /// <param name="photo">Photo To Delete</param>
        void DeletePhoto(Image photo);
    }
}
