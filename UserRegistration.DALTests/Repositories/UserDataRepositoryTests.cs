using Microsoft.EntityFrameworkCore;
using System.Text;
using UserRegistration.DAL;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories;
using Xunit;

namespace UserRegistration.DALTests.Repositories
{
    public class UserDataRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly UserDataRepository _userDataRepository;
        public UserDataRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid())
                .Options;
            _context = new AppDbContext(options);
            _userDataRepository = new UserDataRepository(_context);
        }

        [Fact]
        public void GetAll_NoUserDataItems_ReturnsEmpty()
        {
            // Act
            var result = _userDataRepository.GetAll();

            // Assert
            Assert.Empty(result);
        }
        [Fact]
        public void GetAll_SomeUserDataItems_ReturnsAllUserDataItems()
        {
            // Arrange
            var userDataItem1 = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserDataItem1",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                CreatedAt = DateTime.Now,

            };
            var userDataItem2 = new UserData
            {
                Id = 2,
                FirstName = "Other",
                LastName = "UserDataItem2",
                SocialSecurityCode = "987654321",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                CreatedAt = DateTime.Now,

            };
            _context.UserData.Add(userDataItem1);
            _context.UserData.Add(userDataItem2);
            _context.SaveChanges();

            // Act
            var result = _userDataRepository.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void Get_ValidId_ReturnsCorrectUserDataItem()
        {
            // Arrange
            var UserData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserDataItem1",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                SocialSecurityCode = "123456789",
                CreatedAt = DateTime.Now,

            };
            _context.UserData.Add(UserData);
            _context.SaveChanges();

            // Act
            var result = _userDataRepository.Get(1);

            // Assert
            Assert.Equal(UserData, result);
        }
        [Fact]
        public void Get_InvalidId_ReturnsNull()
        {
            // Act
            var result = _userDataRepository.Get(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_ValidUserDataItem_UserDataItemIsAdded()
        {
            // Arrange
            var UserData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserDataItem1",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                CreatedAt = DateTime.Now,
            };

            // Act
            _userDataRepository.Add(UserData);

            // Assert
            Assert.Equal(UserData, _context.UserData.Find(UserData.Id));
        }

        [Fact]
        public void Update_ValidUserDataItem_UserDataItemIsUpdated()
        {
            // Arrange
            var UserData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserDataItem1",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                CreatedAt = DateTime.Now,
            };
            _context.UserData.Add(UserData);
            _context.SaveChanges();

            UserData.LastName = "NewTitle";

            // Act
            _userDataRepository.Update(UserData);

            // Assert
            Assert.Equal("NewTitle", _context.UserData.Find(UserData.Id)?.LastName);
        }

        [Fact]
        public void Update_UserDataitemNotInDatabase_ThrowsException()
        {
            // Arrange
            var UserData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserDataItem1",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                CreatedAt = DateTime.Now,
            };

            // Act & Assert
            Assert.Throws<DbUpdateConcurrencyException>(() => _userDataRepository.Update(UserData));

        }

        [Fact]
        public void Delete_ValidId_UserDataItemDoesNotExist()
        {
            // Arrange
            var UserData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserDataItem1",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                CreatedAt = DateTime.Now,

            };
            _context.UserData.Add(UserData);
            _context.SaveChanges();

            // Act
            _userDataRepository.Delete(UserData);

            // Assert
            Assert.Null(_context.UserData.Find(UserData.Id));
        }

        [Fact]
        public void Delete_ValidId_UserDataItemAndImageDoesNotExist()
        {
            // Arrange
            var UserData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserDataItem1",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                CreatedAt = DateTime.Now,

            };
            var image = new Image
            {
                Id = 1,
                ImageName = "Image1",
                Content = Encoding.UTF8.GetBytes("ImageBytes1"),
                UserDataItemId = 1
            };
            _context.UserData.Add(UserData);
            _context.Image.Add(image);
            _context.SaveChanges();

            // Act
            _userDataRepository.Delete(UserData);

            // Assert
            Assert.Null(_context.UserData.Find(UserData.Id));
            Assert.Null(_context.Image.Find(image.Id));
        }
    }
}
