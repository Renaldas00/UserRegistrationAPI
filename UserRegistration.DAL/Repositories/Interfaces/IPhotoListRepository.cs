using UserRegistration.DAL.Entities;

namespace UserRegistration.DAL.Repositories.Interfaces
{
    public interface IPhotoListRepository
    {
        /// <summary>
        /// Inserts Photo Into Database
        /// </summary>
        /// <param name="Photo">Photo Data Which Needs To Be Inserted</param>
        /// <returns>Photo List ID</returns>
        int AddPhoto(Photo Photo);
        /// <summary>
        /// Updates Photo From Database
        /// </summary>
        /// <param name="Photo">Photo Data To Be Updated</param>
        void UpdatePhoto(Photo Photo);
        /// <summary>
        /// Deletes Photo From Database
        /// </summary>
        /// <param name="Id">Photo ID To Be Deleted</param>
        /// <summary>
        /// Retrieves User Data List From Database
        /// </summary>
        /// <param name="id">User Data List ID</param>
        /// <returns>User Data List From Database</returns>
        Photo GetUserPhoto(int id);
        /// <summary>
        /// Deletes User Photo
        /// </summary>
        /// <param name="Photo">Photo To Delete</param>
        void DeletePhoto(Photo Photo);
    }
}
