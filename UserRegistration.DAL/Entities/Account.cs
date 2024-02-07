using System.ComponentModel.DataAnnotations;

namespace UserRegistration.DAL.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public string Role { get; set; } = "User";
        public UserData UserData { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
