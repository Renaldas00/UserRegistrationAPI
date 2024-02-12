using System.ComponentModel.DataAnnotations;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdatePhoneNumberRequestDTO
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        
    }
}
