using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateSocSecCodeRequestDTO
    {
        [Required]
        [SocialSecurityCodeValidator]
        public string SocialSecurityCode {  get; set; }
        
    }
}
