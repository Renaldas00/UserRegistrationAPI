using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserRegistration.API.Validators
{
    public class CityValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are handled by [Required] attribute
            }

            string city = value.ToString();

            // Check if city length is within the allowed range
            if (city.Length < 2 || city.Length > 50)
            {
                return new ValidationResult("City must be between 2 and 50 characters long.");
            }

            // Check if city contains any invalid characters
            if (!Regex.IsMatch(city, "^[a-zA-Z\\s-]*$"))
            {
                return new ValidationResult("City must contain only alphabetic characters, spaces, and hyphens.");
            }

            return ValidationResult.Success;
        }
    }
}
