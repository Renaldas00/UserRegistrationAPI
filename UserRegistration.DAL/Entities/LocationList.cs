using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserRegistration.DAL.Entities
{
    public class LocationList
    {
        [Key]
        public int Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Street { get; set; } = string.Empty;
        public string? HouseNumber { get; set; } = string.Empty;
        public string? ApartmentNumber { get; set; } = string.Empty;
        [ForeignKey(nameof(UserLocation))]
        [Required]
        public int UserLocationId { get; set; }
        public UserDataList UserLocation { get; set; } = null!;
    }
}
