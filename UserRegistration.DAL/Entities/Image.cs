using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserRegistration.DAL.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public byte[] Content { get; set; }

        [ForeignKey(nameof(UserDataItem))]
        [Required]
        public int UserDataItemId { get; set; }
        public UserData UserDataItem { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
