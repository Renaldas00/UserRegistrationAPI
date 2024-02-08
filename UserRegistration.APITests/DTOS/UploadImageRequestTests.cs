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
            public static IEnumerable<object[]> ImageNameValidationData =>
                new List<object[]>
                {
            new object[] { null, false }, // Required validation for ImageName
            new object[] { "", false }, // Required validation for ImageName
            new object[] { new string('a', 101), false }, // Exceeds maximum length
            new object[] { "image.png", true }, // Valid ImageName
            new object[] { "photo.jpeg", true }, // Valid ImageName
            new object[] { "picture.gif", true }, // Valid ImageName
                };

            [Theory]
            [MemberData(nameof(ImageNameValidationData))]
            public void ImageName_ValidationTests(string imageName, bool expectedIsValid)
            {
                // Arrange
                var uploadImageRequestDTO = new UploadImageRequestDTO
                {
                    ImageName = imageName,
                    Image = null // IFormFile can be null for this test
                };
                var validationContext = new ValidationContext(uploadImageRequestDTO);
                var validationResults = new List<ValidationResult>();

                // Act
                var result = Validator.TryValidateObject(uploadImageRequestDTO, validationContext, validationResults, true);

                // Assert
                Assert.Equal(expectedIsValid, result);
            }

            public static IEnumerable<object[]> ImageValidationData =>
                new List<object[]>
                {
            new object[] { null, true }, // IFormFile can be null
            new object[] { new FormFile(Stream.Null, 555555550, 5555555550, "file", "image.jpg"), false }, // Exceeds maximum file size
            new object[] { new FormFile(Stream.Null, 0, 0, "file", "image.png"), true }, // Valid image with allowed extension
            new object[] { new FormFile(Stream.Null, 0, 0, "file", "image.jpeg"), true }, // Valid image with allowed extension
            new object[] { new FormFile(Stream.Null, 0, 0, "file", "image.gif"), true }, // Valid image with allowed extension
                };

            [Theory]
            [MemberData(nameof(ImageValidationData))]
            public void Image_ValidationTests(IFormFile image, bool expectedIsValid)
            {
                // Arrange
                var uploadImageRequestDTO = new UploadImageRequestDTO
                {
                    ImageName = "image.png",
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
