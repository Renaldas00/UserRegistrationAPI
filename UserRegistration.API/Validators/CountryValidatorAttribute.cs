using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserRegistration.API.Validators
{
    public class CountryValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are handled by [Required] attribute
            }

            string country = value.ToString();

            // Check if country length is within the allowed range
            if (country.Length < 3 || country.Length > 36)
            {
                return new ValidationResult("Country must be between 4 and 36 characters long.");
            }

            // Check if country contains any special characters or numbers
            if (Regex.IsMatch(country, "[^a-zA-Z ]"))
            {
                return new ValidationResult("Country must not contain special characters or numbers.");
            }

            return ValidationResult.Success;
        }
    }
}
