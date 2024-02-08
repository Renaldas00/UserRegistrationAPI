using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserRegistration.API.Validators
{
    public class FirstNameValidatorAttribute : ValidationAttribute
    {
        private const int MinLength = 3;
        private const int MaxLength = 36;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // Null value is considered invalid
                return new ValidationResult("First name cannot be null.");
            }

            string firstName = value.ToString();

            if (firstName.Length < MinLength || firstName.Length > MaxLength)
            {
                // First name length is not within the allowed range
                return new ValidationResult($"First name must be between {MinLength} and {MaxLength} characters long.");
            }

            // Define regular expression pattern for first name validation
            // Assuming the first name should contain only letters and may include spaces
            string firstNamePattern = @"^[A-Za-z\s]+$";

            if (!Regex.IsMatch(firstName, firstNamePattern))
            {
                // First name contains invalid characters
                return new ValidationResult("First name can only contain letters (A-Z, a-z) and spaces.");
            }

            // First name is valid
            return ValidationResult.Success;
        }
    }
}
