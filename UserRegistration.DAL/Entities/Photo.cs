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
        public int Size { get; set; }
        [Required]
        public byte[] Content { get; set; }

        [ForeignKey(nameof(UserDataListItem))]
        [Required]
        public int UserDataListItemId { get; set; }
        public UserDataList UserDataListItem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
