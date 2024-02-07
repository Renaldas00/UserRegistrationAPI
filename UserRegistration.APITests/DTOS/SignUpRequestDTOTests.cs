using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.API.DTOS.Requests;
using Xunit;

namespace UserRegistration.APITests.DTOS
{
    /// <summary>
    /// Robust Boundary Value Testing (RBVT) implementation for SignUpRequestDTO
    /// </summary>
    public class SignUpRequestDTOTests
    {

        /// <summary>
        /// username input validation RBVT
        /// </summary>
        public static IEnumerable<object[]> UserNameValidationData =>
           new List<object[]>
           {
                new object[] { null, false }, // required validation for the password input testing
                new object[] { "ab", false }, // value just below minimum boundry
                new object[] { "abc", true }, // value on minimum boundry
                new object[] { "abcd", true }, // value just above minimum boundry
                new object[] { new string('a', 3), true }, // value just below maximum boundry
                new object[] { new string('a', 36), true }, // value on maximum boundry
                new object[] { new string('a', 37), false }, // value just above maximum 
                new object[] { new string('a', 25), true }, // value inside middle of boundry
           };


        [Theory]
        [MemberData(nameof(UserNameValidationData))]
        public void UserName_ValidationTests(string userName, bool expectedIsValid)
        {
            // Arrange
            var SignUpRequestDTO = new SignUpRequestDTO
            {
                UserName = userName,
                Password = "P@$$w0rd",
            };
            var validationContext = new ValidationContext(SignUpRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(SignUpRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }


        //---------------------------------------------------------------------------


        /// <summary>
        /// password input validation RBVT
        /// </summary>
        public static IEnumerable<object[]> PasswordValidationData =>
    new List<object[]>
    {
        new object[] { null, false }, // required validation for the password input testing
        new object[] { "A1!", false }, // value just below minimum boundry
        new object[] { "A1!d", true }, // value on minimum boundry
        new object[] { "A1!de", true }, // value just above minimum boundry
        new object[] { "A1!" + new string('a', 3), true }, // value just below maximum boundry
        new object[] { "A1!" + new string('a', 21), true }, // value on maximum boundry
        new object[] { "A1!" + new string('a', 25), false }, // value just above maximum 
        new object[] { "A1!" + new string('a', 12), true }, // value inside middle of boundry
        new object[] { "password", false }, // upper case requirement for the password input testing
        new object[] { "PASSWORD", false }, // lower case requirement for the password input testing
        new object[] { "Password", false }, // digit requirement for the password input testing
        new object[] { "Password1`", true }, // special character requirement for the password input testing
    };
        [Theory]
        [MemberData(nameof(PasswordValidationData))]
        public void Password_ValidationTests(string password, bool expectedIsValid)
        {
            // Arrange
            var SignUpRequestDTO = new SignUpRequestDTO
            {
                UserName = "Abcd",
                Password = password,
            };
            var validationContext = new ValidationContext(SignUpRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(SignUpRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }

        //---------------------------------------------------------------------------
    }
}
