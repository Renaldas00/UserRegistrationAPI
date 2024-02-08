using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserRegistration.API.Validators
{
    public class ImageNameValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Image name is required.");
            }

            string imageName = value.ToString();

            // Check if image name length is within the allowed range
            if (imageName.Length < 3 || imageName.Length > 100)
            {
                return new ValidationResult("Image name must be between 3 and 100 characters long.");
            }

            // Check if image name contains any invalid characters
            if (!Regex.IsMatch(imageName, @"^[a-zA-Z0-9\s\-_\.]*$"))
            {
                return new ValidationResult("Image name must contain only alphanumeric characters, spaces, hyphens, underscores, and periods.");
            }

            return ValidationResult.Success;
        }
    }
}
