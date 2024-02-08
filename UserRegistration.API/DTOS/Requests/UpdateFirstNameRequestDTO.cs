using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateFirstNameRequestDTO
    {
        [Required]
        [FirstNameValidator]
        public string FirstName {  get; set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}
