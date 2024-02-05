using System.ComponentModel.DataAnnotations;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdatePhoneNumberUserDataListRequestDTO
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime UpdatedAt { get; internal set; }
    }
}
