using System.ComponentModel.DataAnnotations;
using UserRegistration.API.DTOS.Requests;
using Xunit;

namespace UserRegistration.APITests.DTOS
{
    /// <summary>
    /// Robust Boundary Value Testing (RBVT) implementation for SignUpRequestDTO.
    /// </summary>
    public class SignUpRequestDTOTests
    {
        /// <summary>
        /// Username input validation RBVT.
        /// </summary>
        public static IEnumerable<object[]> UserNameValidationData =>
            new List<object[]>
            {
                new object[] { null, false }, // required validation for the username input testing
                new object[] { "ab", false }, // value just below minimum boundary
                new object[] { "abc", true }, // value on minimum boundary
                new object[] { "abcd", true }, // value just above minimum boundary
                new object[] { new string('a', 3), true }, // value just below maximum boundary
                new object[] { new string('a', 36), true }, // value on maximum boundary
                new object[] { new string('a', 37), false }, // value just above maximum boundary 
                new object[] { new string('a', 25), true }, // value inside middle of boundary
            };

        /// <summary>
        /// Tests the validation of username input based on RBVT.
        /// </summary>
        [Theory]
        [MemberData(nameof(UserNameValidationData))]
        public void UserName_ValidationTests(string userName, bool expectedIsValid)
        {
            // Arrange
            var signUpRequestDTO = new SignUpRequestDTO
            {
                UserName = userName,
                Password = "P@$$w0rd",
            };
            var validationContext = new ValidationContext(signUpRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(signUpRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }

        /// <summary>
        /// Password input validation RBVT.
        /// </summary>
        public static IEnumerable<object[]> PasswordValidationData =>
            new List<object[]>
            {
                new object[] { null, false }, // required validation for the password input testing
                new object[] { "A1!", false }, // value just below minimum boundary
                new object[] { "A1!d", true }, // value on minimum boundary
                new object[] { "A1!de", true }, // value just above minimum boundary
                new object[] { "A1!" + new string('a', 1), true }, // value just below maximum boundary
                new object[] { "A1!" + new string('a', 20), true }, // value on maximum boundary
                new object[] { "A1!" + new string('a', 22), false }, // value just above maximum 
                new object[] { "A1!" + new string('a', 12), true }, // value inside middle of boundary
                new object[] { "password", false }, // upper case requirement for the password input testing
                new object[] { "PASSWORD", false }, // lower case requirement for the password input testing
                new object[] { "Password", false }, // digit requirement for the password input testing
                new object[] { "Password1`", true }, // special character requirement for the password input testing
            };

        /// <summary>
        /// Tests the validation of password input based on RBVT.
        /// </summary>
        [Theory]
        [MemberData(nameof(PasswordValidationData))]
        public void Password_ValidationTests(string password, bool expectedIsValid)
        {
            // Arrange
            var signUpRequestDTO = new SignUpRequestDTO
            {
                UserName = "Abcd",
                Password = password,
            };
            var validationContext = new ValidationContext(signUpRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(signUpRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }
    }
}
