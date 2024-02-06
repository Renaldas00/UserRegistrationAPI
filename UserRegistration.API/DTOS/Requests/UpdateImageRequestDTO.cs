using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateImageRequestDTO
    {
        [Required]
        [StringLength(100)]
        public string ImageName { get; set; }

        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions([".png"])]
        public IFormFile Image { get; set; } = null!;
    }
}
