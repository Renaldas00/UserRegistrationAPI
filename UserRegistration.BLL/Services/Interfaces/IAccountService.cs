namespace UserRegistration.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Create Password Salt And Hash And Store It In DB
        /// </summary>
        /// <param name="password">Password To Hash</param>
        /// <param name="passwordHash">Password Hash</param>
        /// <param name="passwordSalt">Password Salt</param>
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        /// <summary>
        /// Password Has Verification
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="passwordHash">Password Hash</param>
        /// <param name="passwordSalt">Password Salt</param>
        /// <returns></returns>
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
