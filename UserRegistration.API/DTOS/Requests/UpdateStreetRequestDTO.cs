using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateStreetRequestDTO
    {
        [Required]
        [StreetValidator]
        public string Street { get; set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}
