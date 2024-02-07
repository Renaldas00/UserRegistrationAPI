using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.DAL;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories;
using Xunit;

namespace UserRegistration.DALTests.Repositories
{
    public class UserDataRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly UserDataRepository _todoRepository;

        public UserDataRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid())
                .Options;
            //Ensure that the data seeding is skipped when the context is created
            _context = new AppDbContext(options);
            _todoRepository = new UserDataRepository(_context);
        }

        [Fact]
        public void GetAll_NoTodoItems_ReturnsEmpty()
        {
            // Act
            var result = _todoRepository.GetAll();

            // Assert
            Assert.Empty(result);
        }
        [Fact]
        public void GetAll_SomeTodoItems_ReturnsAllTodoItems()
        {
            // Arrange
            var todoItem1 = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "TodoItem1",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                CreatedAt = DateTime.Now,

            };
            var todoItem2 = new UserData
            {
                Id = 2,
                FirstName = "Other",
                LastName = "TodoItem2",
                SocialSecurityCode = "987654321",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                CreatedAt = DateTime.Now,

            };
            _context.UserData.Add(todoItem1);
            _context.UserData.Add(todoItem2);
            _context.SaveChanges();

            // Act
            var result = _todoRepository.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void Get_ValidId_ReturnsCorrectTodoItem()
        {
            // Arrange
            var UserData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "TodoItem1",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                SocialSecurityCode = "123456789",
                CreatedAt = DateTime.Now,

            };
            _context.UserData.Add(UserData);
            _context.SaveChanges();

            // Act
            var result = _todoRepository.Get(1);

            // Assert
            Assert.Equal(UserData, result);
        }
        [Fact]
        public void Get_InvalidId_ReturnsNull()
        {
            // Act
            var result = _todoRepository.Get(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_ValidTodoItem_TodoItemIsAdded()
        {
            // Arrange
            var UserData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "TodoItem1",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                CreatedAt = DateTime.Now,
            };

            // Act
            _todoRepository.Add(UserData);

            // Assert
            Assert.Equal(UserData, _context.UserData.Find(UserData.Id));
        }

        [Fact]
        public void Update_ValidTodoItem_TodoItemIsUpdated()
        {
            // Arrange
            var UserData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "TodoItem1",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                CreatedAt = DateTime.Now,
            };
            _context.UserData.Add(UserData);
            _context.SaveChanges();

            UserData.LastName = "NewTitle";

            // Act
            _todoRepository.Update(UserData);

            // Assert
            Assert.Equal("NewTitle", _context.UserData.Find(UserData.Id)?.LastName);
        }

        [Fact]
        public void Update_TodoitemNotInDatabase_ThrowsException()
        {
            // Arrange
            var UserData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "TodoItem1",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                CreatedAt = DateTime.Now,
            };

            // Act & Assert
            Assert.Throws<DbUpdateConcurrencyException>(() => _todoRepository.Update(UserData));

        }

        [Fact]
        public void Delete_ValidId_TodoItemDoesNotExist()
        {
            // Arrange
            var UserData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "TodoItem1",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                EmailAddres = "hihn@gmail.com",
                CreatedAt = DateTime.Now,

            };
            _context.UserData.Add(UserData);
            _context.SaveChanges();

            // Act
            _todoRepository.Delete(UserData);

            // Assert
            Assert.Null(_context.UserData.Find(UserData.Id));
        }

        [Fact]
        public void Delete_ValidId_TodoItemAndImageDoesNotExist()
        {
            // Arrange
            var UserData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "TodoItem1",
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
            _todoRepository.Delete(UserData);

            // Assert
            Assert.Null(_context.UserData.Find(UserData.Id));
            Assert.Null(_context.Image.Find(image.Id));
        }
    }
}
