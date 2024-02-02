namespace UserRegistration.BLL.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Generates JWT Token
        /// </summary>
        /// <param name="userId">UUID For JWT Token</param>
        /// <param name="userName">UserName For JWT Token</param>
        /// <param name="role">Role For JWT Token</param>
        /// <returns>User JWT Token String</returns>
        string GetJwtToken(Guid userId, string userName, string role);
    }
}
