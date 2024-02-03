using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class SignUpDTO
    {
        /// <summary>
        /// Username Of The Account
        /// </summary>
        [Required]
        [UserNameValidator]
        public string UserName { get; set; }
        /// <summary>
        /// Email Of The Account
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
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
