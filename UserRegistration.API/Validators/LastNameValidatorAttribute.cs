using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserRegistration.API.Validators
{
    public class LastNameValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // Null value is considered invalid
                return new ValidationResult("Last name cannot be null.");
            }

            string lastName = value.ToString();

            // Define regular expression pattern for last name validation
            // Assuming the last name should contain only letters and may include spaces
            string lastNamePattern = @"^[A-Za-z\s]+$";

            if (!Regex.IsMatch(lastName, lastNamePattern))
            {
                // Last name contains invalid characters
                return new ValidationResult("Last name can only contain letters (A-Z, a-z) and spaces.");
            }

            // Additional validation logic can be added here if needed

            // Last name is valid
            return ValidationResult.Success;
        }
    }
}
