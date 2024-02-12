using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class CreateLocationItemRequestDTO
    {
        [Required]
        [CountryValidator]
        public string Country { get; set; }
        [Required]
        [CityValidator]
        public string? City { get; set; }
        [Required]
        [StreetValidator]
        public string? Street { get; set; }
        [HouseNumberValidator]
        public string? HouseNumber { get; set; }
        [ApartmentNumberValidator]
        public string? ApartmentNumber { get; set; }
        public DateTime CreatedAt { get; internal set; }
    }
}
