using UserRegistration.DAL.Entities;

namespace UserRegistration.API.DTOS.Responses
{
    public class UserDataListResultDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddres { get; set; }
        public string SocialSecurityCode { get; set; }
        public string PhoneNumber { get; set; }

        public byte[] PhotoContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
