using System.ComponentModel.DataAnnotations;

namespace UserRegistration.DAL.Entities
{
    public class Account
    {
        public Account()
        {
            CreatedAt = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public string Role { get; set; } = "User";
        public UserDataList UserDataList { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
