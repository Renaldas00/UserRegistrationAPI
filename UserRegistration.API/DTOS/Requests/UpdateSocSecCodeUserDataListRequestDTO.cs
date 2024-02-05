using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateSocSecCodeUserDataListRequestDTO
    {
        [Required]
        [SocialSecurityCodeValidator]
        public string SocialSecurityCode {  get; set; }

        public DateTime UpdatedAt {  get; internal set; }
    }
}
