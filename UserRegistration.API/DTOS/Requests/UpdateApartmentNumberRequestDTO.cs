using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateApartmentNumberRequestDTO
    {
        [Required]
        [ApartmentNumberValidator]
        public string ApartmentNumber { get; set; }
    }
}
