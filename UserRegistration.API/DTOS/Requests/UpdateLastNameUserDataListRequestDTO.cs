using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateLastNameUserDataListRequestDTO
    {
        [Required]
        [LastNameValidator]
        public string LastName {  get; set; }

        public DateTime UpdatedAt { get; internal set; }
    }
}
