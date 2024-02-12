using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateCountryRequestDTO
    {
        [Required]
        [CountryValidator]
        public string Country { get; set; }
        
    }
}
