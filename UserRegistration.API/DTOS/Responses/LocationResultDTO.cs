using UserRegistration.DAL.Entities;

namespace UserRegistration.API.DTOS.Responses
{
    public class LocationResultDTO
    {
        public int Id { get; set; }
        public string Country { get; set; } 
        public string? City { get; set; }
        public string? Street { get; set; } 
        public string? HouseNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public UserData UserLocation { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
       
    }
}
