using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateCityRequestDTO
    {
        [Required]
        [CityValidator]
        public string City { get; set; }
    }
}
