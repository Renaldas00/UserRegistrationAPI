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
    public class UserDataRequestDTOTests
    {
        [Fact]
        public void FirstName_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = null, //<-- this is testing value
                LastName = "Hams",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void FirstName_WhenEmpty_ShouldFailValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "", //<-- this is testing value
                LastName = "Hams",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void FirstName_WhenLength101_ShouldFailValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = new string('a', 101), //<-- this is testing value
                LastName = "LastName",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void FirstName_WhenHoliday_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Jogn", //<-- this is testing value
                LastName = "LastName",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void FirstName_WhenWork_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Todd", //<-- this is testing value
                LastName = "LastName",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void FirstName_WhenShopping_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Shopping", //<-- this is testing value
                LastName = "LastName",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void FirstName_WhenOther_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Other", //<-- this is testing value
                LastName = "LastName",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void FirstName_WhenUnkownFirstName_ShouldFailValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "UnkownFirstName", //<-- this is testing value
                LastName = "LastName",
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        //------------------- LastName -------------------

        [Fact]
        public void LastName_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = null, //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LastName_WhenEmpty_ShouldFailValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = "", //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LastName_WhenLength101_ShouldFailValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = new string('a', 101), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LastName_WhenLength100_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = new string('a', 100), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        //------------------- SocialSecurityCode -------------------

        [Fact]
        public void SocialSecurityCode_WhenNull_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = "LastName",
                SocialSecurityCode = null, //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void SocialSecurityCode_WhenEmpty_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = "LastName",
                SocialSecurityCode = "", //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void SocialSecurityCode_WhenLength1001_ShouldFailValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = "LastName",
                SocialSecurityCode = new string('a', 1001), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void SocialSecurityCode_WhenLength1000_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = "LastName",
                SocialSecurityCode = new string('a', 1000), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        //------------------- EmailAddres -------------------

        [Fact]
        public void EmailAddres_WhenNull_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = "LastName",
                EmailAddres = null, //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EmailAddres_WhenEmail_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = "LastName",
                EmailAddres = "holiday@gmail.com", //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EmailAddres_WhenLength101_ShouldFailValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = "LastName",
                EmailAddres = new string('a', 101), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EmailAddres_WhenLength100_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = "LastName",
                EmailAddres = new string('a', 100), //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        //------------------- PhoneNumber -------------------

        [Fact]
        public void PhoneNumber_WhenNull_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = "LastName",
                PhoneNumber = null, //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, false);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void PhoneNumber_WhenEqualToToday_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = "LastName",
                PhoneNumber = "+37065247726", //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void PhoneNumber_ShouldPassValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = "LastName",
                PhoneNumber = "+37069999999", //<-- this is testing value
            };
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void PhoneNumber_WhenLessThanToday_ShouldFailValidation()
        {
            // Arrange
            var dto = new UserDataRequestDTO
            {
                FirstName = "Holiday",
                LastName = "LastName",
                PhoneNumber = "+3806344333", //<-- this is testing value
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
