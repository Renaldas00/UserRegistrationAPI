using UserRegistration.DAL.Entities;

namespace UserRegistration.BLL.Interfaces
{
    public interface IUserManagerService
    {
        /// <summary>
        /// Inserts User Into Database
        /// </summary>
        /// <param name="userName">User Name To Be Inserted</param>
        /// <param name="password">Password To Be Inserted</param>
        /// <param name="email">Email To Be Inserted</param>
        /// <returns>Created User</returns>
        User? CreateAccount(string userName, string password, string email);
        /// <summary>
        /// Creates A Hashed Password In Database
        /// </summary>
        /// <param name="password">Password To Hash</param>
        /// <param name="passwordHash">Password Hash</param>
        /// <param name="passwordSalt">Password Salt</param>
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        /// <summary>
        /// Tries To Verify Password And Password Hash From Request To Database
        /// </summary>
        /// <param name="password">Password To Verify</param>
        /// <param name="storedHash">Stored Hashed Password To Verify</param>
        /// <param name="storedSalt">Stored Salt  To Verify</param>
        /// <returns>Boolean Which Determines If The Verification Was Successful Or Not</returns>
        bool TryVerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        /// <summary>
        /// Searches For User In Database Based On Request From FE
        /// </summary>
        /// <param name="userName">Username To Search For</param>
        /// <param name="password">Password  To Search For</param>
        /// <param name="role">Role  To Search For</param>
        /// <param name="userId">UUID  To Search For</param>
        /// <returns>Boolean Which Shows If The Login Was Successful Or Not</returns>
        bool TryLogin(string userName, string password, out string role, out Guid? userId);
        /// <summary>
        /// Retreives Current User Id From Database
        /// </summary>
        /// <returns>User Id</returns>
        int GetCurrentUserId();

    }
}
