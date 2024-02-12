using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateHouseNumberRequestDTO
    {
        [Required]
        [HouseNumberValidator]
        public string HouseNumber { get; set; }
        
    }
}
