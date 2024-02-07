using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateCountryRequestDTO
    {
        public string Country { get; set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}
