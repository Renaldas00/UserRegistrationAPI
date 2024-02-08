
using Microsoft.EntityFrameworkCore;
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
        public void Create_NullAccount_ThrowsException()
        {

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _accountRepository.Create(null));
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
