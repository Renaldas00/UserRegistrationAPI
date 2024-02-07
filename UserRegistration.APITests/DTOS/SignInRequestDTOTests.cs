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
    public class SignInRequestDTOTests
    {
        [Fact]
        public void UserName_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var dto = new SignInDTO
            {
                UserName = null, //<-- this is testing value
                Password = "P@$$w0rd",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UserName_WhenLength2_ShouldFailValidation()
        {
            // Arrange
            var dto = new SignInDTO
            {
                UserName = "ab", //<-- this is testing value
                Password = "P@$$w0rd",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UserName_WhenLength3_ShouldPassValidation()
        {
            // Arrange
            var dto = new SignInDTO
            {
                UserName = "abc", //<-- this is testing value
                Password = "P@$$w0rd",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void UserName_WhenLength36_ShouldPassValidation()
        {
            // Arrange
            var dto = new SignInDTO
            {
                UserName = new string('a', 36), //<-- this is testing value
                Password = "P@$$w0rd",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void UserName_WhenLength51_ShouldFailValidation()
        {
            // Arrange
            var dto = new SignInDTO
            {
                UserName = new string('a', 51), //<-- this is testing value
                Password = "P@$$w0rd",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Password_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var dto = new SignInDTO
            {
                UserName = "abcde",
                Password = null, //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Password_WhenValid_ShouldPassValidation()
        {
            // Arrange
            var dto = new SignInDTO
            {
                UserName = "abcde",
                Password = "P@$$w0rd", //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Password_WhenInvalid_ShouldFailValidation()
        {
            // Arrange
            var dto = new SignInDTO
            {
                UserName = "abcde",
                Password = "p", //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }
    }
}
