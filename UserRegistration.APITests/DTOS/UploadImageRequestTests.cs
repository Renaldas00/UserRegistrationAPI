using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using UserRegistration.API.DTOS.Requests;
using Xunit;

namespace UserRegistration.APITests.DTOS
{
    public class UploadImageRequestTests
    {
        public class UploadImageRequestDTOTests
        {
            public static IEnumerable<object[]> ImageValidationData =>
                new List<object[]>
                {
                    new object[] { null, true }, // IFormFile can be null
                    new object[] { new FormFile(Stream.Null, 76866876860, 5555555555555550, "image.jpg", "image.jpg"), false }, // Exceeds maximum file size
                    new object[] { new FormFile(Stream.Null, 0, 4, "image.png", "image.png"), true }, // Valid image with allowed extension
                    new object[] { new FormFile(Stream.Null, 0, 0, "image.jpeg", "image.jpeg"), true }, // valid image extension
                };

            [Theory]
            [MemberData(nameof(ImageValidationData))]
            public void Image_ValidationTests(IFormFile image, bool expectedIsValid)
            {
                // Arrange
                var uploadImageRequestDTO = new UploadImageRequestDTO
                {
                    Image = image
                };
                var validationContext = new ValidationContext(uploadImageRequestDTO);
                var validationResults = new List<ValidationResult>();

                // Act
                var result = Validator.TryValidateObject(uploadImageRequestDTO, validationContext, validationResults, true);

                // Assert
                Assert.Equal(expectedIsValid, result);
            }
        }
    }
}

