using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserRegistration.API.Validators
{
    public class UserNameValidatorAttribute : ValidationAttribute
    {
        private const int MinLength = 3;
        private const int MaxLength = 36;
        private const string AllowedCharactersPattern = "^[a-zA-Z0-9]*$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // Value is null, so it's invalid
                return new ValidationResult("Username cannot be null.");
            }

            var username = value.ToString();

            if (username.Length < MinLength || username.Length > MaxLength)
            {
                // Username length is not within the allowed range
                return new ValidationResult($"Username must be between {MinLength} and {MaxLength} characters long.");
            }

            if (!Regex.IsMatch(username, AllowedCharactersPattern))
            {
                // Username contains special characters
                return new ValidationResult("Username can only contain letters (English and Latin alphabets) and digits.");
            }

            // Username is valid
            return ValidationResult.Success;
        }
    }
}
