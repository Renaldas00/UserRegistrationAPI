using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserRegistration.DAL.Entities
{
    public class LocationList
    {
        [Key]
        public int Id { get; set; }
        public string Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        [Required]
        public int PersonalDataListId { get; set; }
        [ForeignKey("PersonalDataListId")]
        public PersonalDataList PersonalDataList { get; set; }
    }
}
