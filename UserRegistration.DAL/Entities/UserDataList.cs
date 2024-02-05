using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserRegistration.DAL.Entities
{
    public class UserDataList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string SocialSecurityCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string EmailAddres { get; set; }

        public LocationList Location { get; set; }
        public Photo Photo { get; set; }

        [ForeignKey(nameof(Account))]
        [Required]
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Photo> Photos { get; set; }
        public List<LocationList> locations { get; set; }
    }
}
