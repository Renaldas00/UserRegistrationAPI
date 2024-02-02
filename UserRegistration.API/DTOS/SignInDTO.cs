using System.ComponentModel.DataAnnotations;

namespace UserRegistration.API.DTOS
{
    public class SignInDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
