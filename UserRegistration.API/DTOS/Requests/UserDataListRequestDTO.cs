using System.ComponentModel.DataAnnotations;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.DTOS.Requests
{
    public class UserDataListRequestDTO
    {
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

        [Required]
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}
