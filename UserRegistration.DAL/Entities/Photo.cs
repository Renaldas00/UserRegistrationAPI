using System.ComponentModel.DataAnnotations;

namespace UserRegistration.DAL.Entities
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        public string ContentType { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public byte[] Content { get; set; }
    }
}
