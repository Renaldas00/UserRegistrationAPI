using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserRegistration.API.Validators
{
    public class HouseNumberValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are handled by [Required] attribute
            }

            string houseNumber = value.ToString();

            // Check if house number length is within the allowed range
            if (houseNumber.Length > 10)
            {
                return new ValidationResult("House number must be at most 10 characters long.");
            }

            // Check if house number contains any invalid characters
            if (!Regex.IsMatch(houseNumber, "^[a-zA-Z0-9\\s.-]*$"))
            {
                return new ValidationResult("House number must contain only alphanumeric characters, spaces, hyphens, and periods.");
            }

            return ValidationResult.Success;
        }
    }
}
