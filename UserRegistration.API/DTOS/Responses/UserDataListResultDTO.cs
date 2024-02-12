namespace UserRegistration.API.DTOS.Responses
{
    public class UserDataListResultDTO
    {
        public int Id { get; set; }

        public int ImageListId { get; set; }

        public int LocationListId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageName { get; set; }
        public string EmailAddres { get; set; }
        public string SocialSecurityCode { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Image { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public DateTime CreatedAt { get; set; }
       
    }
}
