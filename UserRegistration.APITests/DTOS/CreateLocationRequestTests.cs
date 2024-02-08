using System.ComponentModel.DataAnnotations;
using UserRegistration.API.DTOS.Requests;
using Xunit;

namespace UserRegistration.APITests.DTOS
{
    public class CreateLocationRequestTests
    {
        public static IEnumerable<object[]> CountryValidationData =>
       new List<object[]>
       {
            new object[] { "USA", true }, // Valid country
            new object[] { "United States of America", true }, // Valid country with spaces
            new object[] { "1234", false }, // Country with numbers
            new object[] { "", false }, // Empty country
            new object[] { null, false }, // Null country
            new object[] { "Abc@", false }, // Country with special characters
       };

        [Theory]
        [MemberData(nameof(CountryValidationData))]
        public void Country_ValidationTests(string country, bool expectedIsValid)
        {
            // Arrange
            var createLocationItemRequestDTO = new CreateLocationItemRequestDTO
            {
                Country = country,
                City = "New York",
                Street = "Broadway",
                HouseNumber = "123",
                ApartmentNumber = "A101"
            };
            var validationContext = new ValidationContext(createLocationItemRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(createLocationItemRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }
        public static IEnumerable<object[]> CityValidationData =>
        new List<object[]>
        {
            new object[] { "New York", true }, // Valid city
            new object[] { "Los Angeles", true }, // Valid city
            new object[] { "1234", false }, // City with numbers
            new object[] { "", false }, // Empty city
            new object[] { null, false }, // Null city
            new object[] { "Paris@", false }, // City with special characters
            new object[] { "Long City Name with Many Words That Will Not Pass Because It Only Allows I Believe One Hundred Characters And This Is More than That", false }, // Long city name
        };

        [Theory]
        [MemberData(nameof(CityValidationData))]
        public void City_ValidationTests(string city, bool expectedIsValid)
        {
            // Arrange
            var createLocationItemRequestDTO = new CreateLocationItemRequestDTO
            {
                Country = "USA",
                City = city,
                Street = "Broadway",
                HouseNumber = "123",
                ApartmentNumber = "A101"
            };
            var validationContext = new ValidationContext(createLocationItemRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(createLocationItemRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }
        public static IEnumerable<object[]> StreetValidationData =>
        new List<object[]>
        {
            new object[] { "Main Street", true }, // Valid street name
            new object[] { "1st Avenue", true }, // Valid street name with numbers
            new object[] { "", false }, // Empty street
            new object[] { null, false }, // Null street
            new object[] { "Broadway&", false }, // Street with special characters
            new object[] { "Long Street Name with Many Words That Will Not Pass Because It Only Allows I Believe One Hundred Characters And This Is More than That", false }, // Long street name
        };

        [Theory]
        [MemberData(nameof(StreetValidationData))]
        public void Street_ValidationTests(string street, bool expectedIsValid)
        {
            // Arrange
            var createLocationItemRequestDTO = new CreateLocationItemRequestDTO
            {
                Country = "USA",
                City = "New York",
                Street = street,
                HouseNumber = "123",
                ApartmentNumber = "A101"
            };
            var validationContext = new ValidationContext(createLocationItemRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(createLocationItemRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }

        public static IEnumerable<object[]> HouseNumberValidationData =>
        new List<object[]>
        {
            new object[] { "123", true }, // Valid house number
            new object[] { "Apt 101", true }, // House number with apartment identifier
            new object[] { "", false }, // Empty house number
            new object[] { null, true }, // Null house number
            new object[] { "42$", false }, // House number with special characters
            new object[] { "Unit 5B", true }, // House number with alphanumeric characters
            new object[] { "Penthouse", true }, // House number with letters
            new object[] { "Suite 3A", true }, // House number with letters and numbers
            new object[] { "1234567890", true }, // Numeric house number
            new object[] { "Building 6, Floor 2", false }, // Long house number
        };

        [Theory]
        [MemberData(nameof(HouseNumberValidationData))]
        public void HouseNumber_ValidationTests(string houseNumber, bool expectedIsValid)
        {
            // Arrange
            var createLocationItemRequestDTO = new CreateLocationItemRequestDTO
            {
                Country = "USA",
                City = "New York",
                Street = "Main Street",
                HouseNumber = houseNumber,
                ApartmentNumber = "A101"
            };
            var validationContext = new ValidationContext(createLocationItemRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(createLocationItemRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }
        public static IEnumerable<object[]> ApartmentNumberValidationData =>
        new List<object[]>
        {
            new object[] { "A101", true }, // Valid apartment number
            new object[] { "", true }, // Empty apartment number
            new object[] { null, true }, // Null apartment number
            new object[] { "Apt 10", true }, // Apartment number with space
            new object[] { "42$", false }, // Apartment number with special characters
            new object[] { "Unit 5B", true }, // Apartment number with alphanumeric characters
            new object[] { "Penthouse", true }, // Apartment number with letters
            new object[] { "Suite 3A", true }, // Apartment number with letters and space
            new object[] { "1234567890", true }, // Numeric apartment number
            new object[] { "Building 6, Floor 2", false }, // Long apartment number
        };

        [Theory]
        [MemberData(nameof(ApartmentNumberValidationData))]
        public void ApartmentNumber_ValidationTests(string apartmentNumber, bool expectedIsValid)
        {
            // Arrange
            var createLocationItemRequestDTO = new CreateLocationItemRequestDTO
            {
                Country = "USA",
                City = "New York",
                Street = "Main Street",
                HouseNumber = "123",
                ApartmentNumber = apartmentNumber
            };
            var validationContext = new ValidationContext(createLocationItemRequestDTO);
            var validationResults = new List<ValidationResult>();

            // Act
            var result = Validator.TryValidateObject(createLocationItemRequestDTO, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, result);
        }
    }
}
