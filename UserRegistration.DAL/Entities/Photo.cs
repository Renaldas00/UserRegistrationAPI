using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        //[Required]
        public int Size { get; set; } 
        //[Required]
        public byte[] Content { get; set; } = new byte[0];
        [ForeignKey(nameof(UserPhoto))]
        [Required]
        public int UserPhotoId { get; set; }
        public UserDataList UserPhoto { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
