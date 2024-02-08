using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserRegistration.API.Validators
{
    public class ApartmentNumberValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success; // Null or empty values are considered valid
            }

            string apartmentNumber = value.ToString();

            // Check if apartment number length is within the allowed range
            if (apartmentNumber.Length > 10)
            {
                return new ValidationResult("Apartment number must be at most 10 characters long.");
            }

            // Check if apartment number contains any invalid characters
            if (!Regex.IsMatch(apartmentNumber, "^[a-zA-Z0-9\\s.-]*$"))
            {
                return new ValidationResult("Apartment number must contain only alphanumeric characters, spaces, hyphens, and periods.");
            }

            return ValidationResult.Success;
        }
    }
}
