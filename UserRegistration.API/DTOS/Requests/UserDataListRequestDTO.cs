using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UserDataListRequestDTO
    {
        /// <summary>
        /// First Name For User Profile
        /// </summary>
        [Required]
        [FirstNameValidator]
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name For User Profile
        /// </summary>
        [Required]
        [LastNameValidator]
        public string LastName { get; set; }
        /// <summary>
        /// Social Security Code For User Profile
        /// </summary>
        [Required]
        [SocialSecurityCodeValidator]
        public string SocialSecurityCode { get; set; }
        /// <summary>
        /// Phone Number For User Profile
        /// </summary>
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Email Address For User Profile
        /// </summary>
        [Required]
        [EmailAddress]
        public string EmailAddres { get; set; }

        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}
