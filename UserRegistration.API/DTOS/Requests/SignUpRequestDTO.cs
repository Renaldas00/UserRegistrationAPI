using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class SignUpRequestDTO
    {
        /// <summary>
        /// Username Of The Account
        /// </summary>
        [Required]
        [UserNameValidator]
        public string UserName { get; set; }
        /// <summary>
        /// Password Of The Account
        /// </summary>
        [Required]
        [PasswordValidator]
        public string Password { get; set; }
        /// <summary>
        /// Default Role Of The Account
        /// </summary>
        [RoleValidator]
        public string Role { get; } = "User";
    }
}
