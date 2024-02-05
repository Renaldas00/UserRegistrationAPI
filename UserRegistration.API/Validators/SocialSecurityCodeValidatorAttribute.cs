using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserRegistration.API.Validators
{
    public class SocialSecurityCodeValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // Null value is considered invalid
                return new ValidationResult("Social Security Code cannot be null.");
            }

            string ssn = value.ToString();

            // Define regular expression pattern for Social Security Code validation
            // Assuming the Social Security Code format is xxx-xx-xxxx or xxxxxxxxx
            string ssnPattern = @"^\d{3}-?\d{2}-?\d{4}$";

            if (!Regex.IsMatch(ssn, ssnPattern))
            {
                // Social Security Code does not match the expected format
                return new ValidationResult("Social Security Code must be in the format xxx-xx-xxxx or xxxxxxxxx.");
            }

            // Additional validation logic can be added here if needed

            // Social Security Code is valid
            return ValidationResult.Success;
        }
    }
}
