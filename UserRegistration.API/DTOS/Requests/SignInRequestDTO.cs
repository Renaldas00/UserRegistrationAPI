using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class SignInDTO
    {
        /// <summary>
        /// Account UserName
        /// </summary>
        [Required]
        [UserNameValidator]
        public string UserName { get; set; }
        /// <summary>
        /// Maching Account Password
        /// </summary>
        [Required]
        [PasswordValidator]
        public string Password { get; set; }
    }
}
