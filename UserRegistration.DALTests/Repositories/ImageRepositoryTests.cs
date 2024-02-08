using Microsoft.EntityFrameworkCore;
using System.Text;
using UserRegistration.DAL;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories;
using Xunit;

namespace UserRegistration.DALTests.Repositories
{
    public class ImageRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly ImageRepository _imageRepository;

        public ImageRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid())
                .Options;
            //Ensure that the data seeding is skipped when the context is created
            _context = new AppDbContext(options);
            _imageRepository = new ImageRepository(_context);
        }

        [Fact]
        public void GetAll_NoImages_ReturnsEmpty()
        {
            // Act
            var result = _imageRepository.GetAll();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAll_IncludeUserData_ReturnsImageWithUserData()
        {
            // Arrange
            var todoItem = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserData1",
                EmailAddres = "dasdsa@gmail.com",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                CreatedAt = DateTime.Now,

            };
            var image1 = new Image
            {
                Id = 1,
                ImageName = "Image1",
                
                Content = Encoding.UTF8.GetBytes("Content1"),
                UserDataItemId = 1
            };
            _context.UserData.Add(todoItem);
            _context.Image.Add(image1);
            _context.SaveChanges();

            // Act
            var result = _imageRepository.GetAll(i => i.UserDataItem).First();

            // Assert
            Assert.NotNull(result.UserDataItem);
            Assert.Equal("UserData1", result.UserDataItem.LastName);
        }
        [Fact]
        public void Get_ValidId_ReturnsCorrectImage()
        {
            // Arrange
            var todoItem = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserData1",
                EmailAddres = "dasdsa@gmail.com",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                CreatedAt = DateTime.Now,

            };
            var image = new Image
            {
                Id = 1,
                ImageName = "Image1",
                
                Content = Encoding.UTF8.GetBytes("Content1"),
                UserDataItemId = 1
            };
            _context.UserData.Add(todoItem);
            _context.Image.Add(image);
            _context.SaveChanges();

            // Act
            var result = _imageRepository.Get(image.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(image.ImageName, result.ImageName);

        }
        [Fact]
        public void Get_InvalidId_ReturnsNull()
        {
            // Act
            var result = _imageRepository.Get(1);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public void Add_ValidImage_ReturnsNonNullId()
        {
            // Arrange
            var image = new Image
            {
                ImageName = "Image1",
                
                Content = Encoding.UTF8.GetBytes("Content1"),
                UserDataItemId = 1
            };

            // Act
            _imageRepository.Add(image);

            // Assert
            Assert.NotEqual(0, image.Id);
        }
        [Fact]
        public void Update_ValidImage_ChangesAreSaved()
        {
            // Arrange
            var todoItem = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserData1",
                EmailAddres = "dasdsa@gmail.com",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                CreatedAt = DateTime.Now,

            };
            var image = new Image
            {
                Id = 1,
                ImageName = "Image1",
                
                Content = Encoding.UTF8.GetBytes("Content1"),
                UserDataItemId = 1
            };
            _context.UserData.Add(todoItem);
            _context.Image.Add(image);
            _context.SaveChanges();

            // Act
            image.ImageName = "NewImageName";
            _imageRepository.Update(image);

            // Assert
            var result = _imageRepository.Get(image.Id);
            Assert.Equal("NewImageName", result.ImageName);
        }
        [Fact]
        public void Update_ImageNotInDatabase_ThrowsException()
        {
            // Arrange
            var image = new Image
            {
                Id = 1,
                ImageName = "Image1",
                
                Content = Encoding.UTF8.GetBytes("Content1"),
                UserDataItemId = 1
            };

            // Act & Assert
            Assert.Throws<DbUpdateConcurrencyException>(() => _imageRepository.Update(image));
        }
        [Fact]
        public void Delete_ValidId_ImageDoesNotExist()
        {
            // Arrange
            var todoItem = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserData1",
                EmailAddres = "dasdsa@gmail.com",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                CreatedAt = DateTime.Now,

            };
            var image = new Image
            {
                Id = 1,
                ImageName = "Image1",
                
                Content = Encoding.UTF8.GetBytes("Content1"),
                UserDataItemId = 1
            };
            _context.UserData.Add(todoItem);
            _context.Image.Add(image);
            _context.SaveChanges();

            // Act
            _imageRepository.Delete(image);

            // Assert
            Assert.Null(_context.Image.Find(image.Id));
        }
        [Fact]
        public void Delete_ImageNotInDatabase_ThrowsException()
        {
            // Arrange
            var image = new Image
            {
                Id = 1,
                ImageName = "Image1",
                
                Content = Encoding.UTF8.GetBytes("Content1"),
                UserDataItemId = 1
            };

            // Act & Assert
            Assert.Throws<DbUpdateConcurrencyException>(() => _imageRepository.Delete(image));
        }
        [Fact]
        public void Delete_ValidId_ImageDoesNotExistButUserDataExist()
        {
            // Arrange
            var todoItem = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserData1",
                EmailAddres = "dasdsa@gmail.com",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                CreatedAt = DateTime.Now,

            };
            var image = new Image
            {
                Id = 1,
                ImageName = "Image1",
                Content = Encoding.UTF8.GetBytes("Content1"),
                UserDataItemId = 1
            };
            _context.UserData.Add(todoItem);
            _context.Image.Add(image);
            _context.SaveChanges();

            // Act
            _imageRepository.Delete(image);

            // Assert
            Assert.Null(_context.Image.Find(image.Id));
            Assert.NotNull(_context.UserData.Find(todoItem.Id));

        }
    }
}
