using System.ComponentModel.DataAnnotations;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateEmailAddressUserDataListRequestDTO
    {
        [Required]
        [EmailAddress]
        public string EmailAdress { get; set; }

        public DateTime UpdatedAt { get; internal set; }
    }
}
