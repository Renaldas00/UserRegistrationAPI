using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UploadImageRequestDTO
    {
        //[Required]
        //[StringLength(100)]
        public string ImageName { get; set; } 

        //[Required]
        //[StringLength(1000)]
        public string? Description { get; set; } = string.Empty;

        //[MaxFileSize(5 * 1024 * 1024)]
        //[AllowedExtensions([".png"])]
        public IFormFile Image { get; set; }

        public string ContentType { get; set; } = string.Empty;
        //[Required]
        public int Size { get; set; }
        //[Required]
        public byte[] Content {  get; set; }
    }
}
