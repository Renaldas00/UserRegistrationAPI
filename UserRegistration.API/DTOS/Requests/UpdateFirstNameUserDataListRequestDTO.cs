using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateFirstNameUserDataListRequestDTO
    {
        [Required]
        [FirstNameValidator]
        public string FirstName {  get; set; }

        public DateTime UpdatedAt { get; internal set; }
    }
}
