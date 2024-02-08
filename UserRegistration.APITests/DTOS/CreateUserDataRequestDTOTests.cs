using System.ComponentModel.DataAnnotations;
using UserRegistration.API.DTOS.Requests;
using Xunit;

namespace UserRegistration.APITests.DTOS
{
    public class CreateUserDataRequestDTOTests
    {
        /// <summary>
        /// Data for validating first name input.
        /// </summary>
        public static IEnumerable<object[]> FirstNameValidationData =>
            new List<object[]>
            {
            new object[] { null, false }, // required validation for the first name input testing
            new object[] { "Tod", true }, // valid first name
            new object[] { "John123", false }, // first name containing digits
            new object[] { "John Doe", true }, // first name containing space
            new object[] { "J", false }, // first name too short
            new object[] { new string('a', 36), true }, // value on maximum boundary
            new object[] { new string('a', 37), false }, // value just above maximum boundary 
            };

        /// <summary>
        /// Tests the validation of first name input.
        /// </summary>
        [Theory]
        [MemberData(nameof(FirstNameValidationData))]
        public void FirstName_ValidationTests(string firstName, bool expectedIsValid)
        {
            // Arrange
            var createUserDataRequestDTO = new CreateUserDataRequestDTO
            {
                FirstName = firstName,
                LastName = "Doe",
                SocialSecurityCode = "123-45-6789",
                PhoneNumber = "1234567890",
                EmailAddres = "john.doe@example.com",
            };
            var validationContext = new ValidationContext(createUserDataRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(createUserDataRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }

        /// <summary>
        /// Data for validating last name input.
        /// </summary>
        public static IEnumerable<object[]> LastNameValidationData =>
            new List<object[]>
            {
            new object[] { null, false }, // required validation for the last name input testing
            new object[] { "Doe", true }, // valid last name
            new object[] { "Doe123", false }, // last name containing digits
            new object[] { "Doe Jr.", true }, // last name containing space and period
            new object[] { "D", false }, // last name too short
            new object[] { new string('a', 36), true }, // value on maximum boundary
            new object[] { new string('a', 37), false }, // value just above maximum boundary 
            };

        /// <summary>
        /// Tests the validation of last name input.
        /// </summary>
        [Theory]
        [MemberData(nameof(LastNameValidationData))]
        public void LastName_ValidationTests(string lastName, bool expectedIsValid)
        {
            // Arrange
            var createUserDataRequestDTO = new CreateUserDataRequestDTO
            {
                FirstName = "John",
                LastName = lastName,
                SocialSecurityCode = "123-45-6789",
                PhoneNumber = "1234567890",
                EmailAddres = "john.doe@example.com",
            };
            var validationContext = new ValidationContext(createUserDataRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(createUserDataRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }

        /// <summary>
        /// Data for validating social security code input.
        /// </summary>
        public static IEnumerable<object[]> SocialSecurityCodeValidationData =>
            new List<object[]>
            {
            new object[] { null, false }, // required validation for the social security code input testing
            new object[] { "123-45-6789", true }, // valid social security code
            new object[] { "123456789", true }, // social security code without dashes
            new object[] { "1234567890", false }, // social security code with invalid length
            new object[] { "12345-6789a", false }, // social security code with invalid format
            new object[] { "12345-67890", false }, // social security code with invalid format
            };

        /// <summary>
        /// Tests the validation of social security code input.
        /// </summary>
        [Theory]
        [MemberData(nameof(SocialSecurityCodeValidationData))]
        public void SocialSecurityCode_ValidationTests(string ssn, bool expectedIsValid)
        {
            // Arrange
            var createUserDataRequestDTO = new CreateUserDataRequestDTO
            {
                FirstName = "John",
                LastName = "Doe",
                SocialSecurityCode = ssn,
                PhoneNumber = "1234567890",
                EmailAddres = "john.doe@example.com",
            };
            var validationContext = new ValidationContext(createUserDataRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(createUserDataRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }

        /// <summary>
        /// Data for validating phone number input.
        /// </summary>
        public static IEnumerable<object[]> PhoneNumberValidationData =>
        new List<object[]>
        {
            new object[] { null, false }, // Required validation for the phone number input testing
            new object[] { "123", true }, // Minimum length boundary
            new object[] { "1234567890123456789012345678901234567890", true }, // Maximum length boundary
            new object[] { "1234", true }, // Just above the minimum length boundary
            new object[] { "12345abc", false }, // Invalid characters
            new object[] { "123-456-7890", true }, // Valid with hyphens
            new object[] { "123.456.7890", true }, // Valid with dots
            new object[] { "123(456)7890", true }, // Valid with parentheses
            new object[] { "123 456 7890", true }, // Valid with whitespace
            new object[] { "123 456 7890 x1234", true }, // Valid with extension abbreviation 'x'
            new object[] { "123 456 7890 EXT1234", true }, // Valid with extension abbreviation 'EXT'
            new object[] { "123 456 7890 EXT.1234", true }, // Valid with extension abbreviation 'EXT.'
            new object[] { "123 456 7890 ext.1a34", false }, // Invalid extension with non-digits
            new object[] { "123 456 7890 ext.123", true }, // Valid extension with 3 digits
            new object[] { "123 456 7890 ext.1", true }, // Valid extension with 1 digit
            new object[] { "869999999", true }, // Lithuanian Number
            new object[] { "+37069999999", true }, // Lithuanian Number
        };

        /// <summary>
        /// Tests the validation of phone number input.
        /// </summary>
        [Theory]
        [MemberData(nameof(PhoneNumberValidationData))]
        public void PhoneNumber_ValidationTests(string phoneNumber, bool expectedIsValid)
        {
            // Arrange
            var createUserDataRequestDTO = new CreateUserDataRequestDTO
            {
                FirstName = "John",
                LastName = "Doe",
                SocialSecurityCode = "123-45-6789",
                PhoneNumber = phoneNumber,
                EmailAddres = "john.doe@example.com",
            };
            var validationContext = new ValidationContext(createUserDataRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(createUserDataRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }

        /// <summary>
        /// Data for validating email address input.
        /// </summary>
        public static IEnumerable<object[]> EmailAddressValidationData =>
            new List<object[]>
            {
            new object[] { null, false }, // required validation for the email address input testing
            new object[] { "john.doe@example.com", true }, // valid email address
            new object[] { "john.doe", false }, // email address without domain
            new object[] { "john.doe@", false }, // email address without domain
            new object[] { "john.doe@example", true }, // email address without top-level domain
            new object[] { "john.doe@example.", true }, // email address without top-level domain
            };

        /// <summary>
        /// Tests the validation of email address input.
        /// </summary>
        [Theory]
        [MemberData(nameof(EmailAddressValidationData))]
        public void EmailAddress_ValidationTests(string emailAddress, bool expectedIsValid)
        {
            // Arrange
            var createUserDataRequestDTO = new CreateUserDataRequestDTO
            {
                FirstName = "John",
                LastName = "Doe",
                SocialSecurityCode = "123-45-6789",
                PhoneNumber = "1234567890",
                EmailAddres = emailAddress,
            };
            var validationContext = new ValidationContext(createUserDataRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(createUserDataRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }
    }
}
