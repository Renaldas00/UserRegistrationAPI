namespace UserRegistration.API.DTOS.Requests
{
    public class LocationItemRequestDTO
    {
        public string Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime? UpdatedAt { get; internal set; }
    }
}
