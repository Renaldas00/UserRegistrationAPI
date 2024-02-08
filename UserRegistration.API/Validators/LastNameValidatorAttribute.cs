using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserRegistration.API.Validators
{
    public class LastNameValidatorAttribute : ValidationAttribute
    {
        private const int MinLength = 3;
        private const int MaxLength = 36;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // Null value is considered invalid
                return new ValidationResult("Last name cannot be null.");
            }

            string lastName = value.ToString();

            if (lastName.Length < MinLength || lastName.Length > MaxLength)
            {
                // Last name length is not within the allowed range
                return new ValidationResult($"Last name must be between {MinLength} and {MaxLength} characters long.");
            }

            // Define regular expression pattern for last name validation
            // Allowing letters (A-Z, a-z), spaces, and periods
            string lastNamePattern = @"^[A-Za-z\s.]+$";

            if (!Regex.IsMatch(lastName, lastNamePattern))
            {
                // Last name contains invalid characters
                return new ValidationResult("Last name can only contain letters (A-Z, a-z), spaces, and periods.");
            }

            // Last name is valid
            return ValidationResult.Success;
        }
    }
}
