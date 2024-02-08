using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserRegistration.API.Validators
{
    public class StreetValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are handled by [Required] attribute
            }

            string street = value.ToString();

            // Check if street length is within the allowed range
            if (street.Length < 2 || street.Length > 100)
            {
                return new ValidationResult("Street must be between 2 and 100 characters long.");
            }

            // Check if street contains any invalid characters
            if (!Regex.IsMatch(street, "^[a-zA-Z0-9\\s,.'-]*$"))
            {
                return new ValidationResult("Street must contain only alphanumeric characters, spaces, commas, periods, hyphens, and apostrophes.");
            }

            return ValidationResult.Success;
        }
    }
}
