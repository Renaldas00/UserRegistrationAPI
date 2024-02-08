namespace UserRegistration.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Creates a password hash and salt for the given password and stores them in the database.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="passwordHash">Output parameter for the generated password hash.</param>
        /// <param name="passwordSalt">Output parameter for the generated password salt.</param>
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        /// <summary>
        /// Verifies the provided password against the stored password hash and salt.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="passwordHash">The stored password hash.</param>
        /// <param name="passwordSalt">The stored password salt.</param>
        /// <returns>True if the password is verified; otherwise, false.</returns>
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
