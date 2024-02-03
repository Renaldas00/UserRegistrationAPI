﻿
using Microsoft.EntityFrameworkCore;
using System.Text;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;
using UserRegistration.DAL.Repositories;
using Xunit;
using UserRegistration.DAL;

namespace UserRegistration.DALTests.Repositories
{
    public class AccountRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly IAccountRepository _accountRepository;

        public AccountRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase" + Guid.NewGuid())
                .Options;
            _context = new AppDbContext(options);
            _accountRepository = new AccountRepository(_context);
        }
        [Fact]
        public void Create_ValidAccount_ReturnsNonNullId()
        {
            // Arrange
            var account = new Account { UserName = "testUser", Email = "testEmail@test.com", PasswordHash = Encoding.UTF8.GetBytes("hash"), PasswordSalt = Encoding.UTF8.GetBytes("salt"), Role = "User" };

            // Act
            var result = _accountRepository.Create(account);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public void Create_NullAccount_ThrowsException()
        {

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _accountRepository.Create(null));
        }

        [Fact]
        public void Create_DuplicateUserName_ThrowsException()
        {
            // Arrange
            var account1 = new Account { UserName = "testUser", Email = "testEmail1@test.com", PasswordHash = Encoding.UTF8.GetBytes("hash"), PasswordSalt = Encoding.UTF8.GetBytes("salt"), Role = "User" };
            var account2 = new Account { UserName = "testUser", Email = "testEmail2@test.com", PasswordHash = Encoding.UTF8.GetBytes("hash"), PasswordSalt = Encoding.UTF8.GetBytes("salt"), Role = "User" };

            // Act
            _accountRepository.Create(account1);

            // Assert
            Assert.Throws<ArgumentException>(() => _accountRepository.Create(account2));
        }

        [Fact]
        public void Get_ValidUserName_ReturnsCorrectAccount()
        {
            // Arrange
            var account = new Account
            {
                UserName = "testUser",
                Email = "testEmail@test.com",
                PasswordHash = Encoding.UTF8.GetBytes("fakePasswordHash"),
                PasswordSalt = Encoding.UTF8.GetBytes("fakePasswordSalt"),
                Role = "user"
            };
            _context.Account.Add(account);
            _context.SaveChanges();

            // Act
            var result = _accountRepository.Get(account.UserName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(account.UserName, result.UserName);
        }

        [Fact]
        public void Get_InvalidUserName_ReturnsNull()
        {
            // Act
            var result = _accountRepository.Get("invalidUserName");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Get_NullUserName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _accountRepository.Get(null));
        }

        [Fact]
        public void Delete_ValidId_AccountDoesNotExist()
        {
            // Arrange
            var account = new Account
            {
                UserName = "testUser",
                Email = "testEmail@test.com",
                PasswordHash = Encoding.UTF8.GetBytes("fakePasswordHash"),
                PasswordSalt = Encoding.UTF8.GetBytes("fakePasswordSalt"),
                Role = "user"
            };
            _context.Account.Add(account);
            _context.SaveChanges();

            var id = account.Id;

            // Act
            _accountRepository.Delete(id);

            // Assert
            Assert.False(_accountRepository.Exists(id));
        }

        [Fact]
        public void Delete_InvalidId_NoExceptionThrown()
        {
            // Arrange
            var invalidId = Guid.NewGuid();

            // Act & Assert
            var exception = Record.Exception(() => _accountRepository.Delete(invalidId));
            Assert.Null(exception);
            Assert.False(_accountRepository.Exists(invalidId));
        }
    }
}
