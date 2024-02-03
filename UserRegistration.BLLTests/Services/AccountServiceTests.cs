using UserRegistration.BLL.Services;
using Xunit;

namespace UserRegistration.BLLTests.Services
{
    public class AccountServiceTests
    {
        [Fact]
        public void VerifyPasswordHash_ValidPassword_ReturnsTrue()
        {
            // Arrange
            var password = "validPassword";
            byte[] passwordHash, passwordSalt;
            var accountService = new AccountService();
            accountService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Act
            var result = accountService.VerifyPasswordHash(password, passwordHash, passwordSalt);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPasswordHash_IncorrectHashAndSalt_ReturnsFalse()
        {
            // Arrange
            var password = "validPassword";
            byte[] passwordHash, passwordSalt;
            var accountService = new AccountService();
            accountService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Act
            var result = accountService.VerifyPasswordHash("wrongPassword", passwordHash, passwordSalt);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyPasswordHash_NullPassword_ThrowsException()
        {
            // Arrange
            var accountService = new AccountService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => accountService.VerifyPasswordHash(null, new byte[0], new byte[0]));
        }

        [Fact]
        public void VerifyPasswordHash_NullHashAndSalt_ThrowsException()
        {
            // Arrange
            var accountService = new AccountService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => accountService.VerifyPasswordHash("validPassword", null, null));
        }

        [Fact]
        public void CreatePasswordHash_ValidPassword_ReturnsNonNullHashAndSalt()
        {
            // Arrange
            var password = "validPassword";
            var accountService = new AccountService();

            // Act
            accountService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            // Assert
            Assert.NotNull(passwordHash);
            Assert.NotNull(passwordSalt);
            Assert.NotEmpty(passwordHash);
            Assert.NotEmpty(passwordSalt);
        }

        [Fact]
        public void CreatePasswordHash_NullPassword_ThrowsException()
        {
            // Arrange
            var accountService = new AccountService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => accountService.CreatePasswordHash(null, out _, out _));
        }

        [Fact]
        public void CreatePasswordHash_SamePasswords_ReturnsDifferentHashAndSalt()
        {
            // Arrange
            var password0 = "validPassword0";
            var password1 = "validPassword0";
            var accountService = new AccountService();

            // Act
            accountService.CreatePasswordHash(password0, out byte[] passwordHash0, out byte[] passwordSalt0);
            accountService.CreatePasswordHash(password1, out byte[] passwordHash1, out byte[] passwordSalt1);

            // Assert
            Assert.NotEqual(passwordHash0, passwordHash1);
            Assert.NotEqual(passwordSalt0, passwordSalt1);
        }

        [Fact]
        public void CreatePasswordHash_DifferentPasswords_ReturnsDifferentHashAndSalt()
        {
            // Arrange
            var password0 = "validPassword0";
            var password1 = "validPassword1";
            var accountService = new AccountService();

            // Act
            accountService.CreatePasswordHash(password0, out byte[] passwordHash0, out byte[] passwordSalt0);
            accountService.CreatePasswordHash(password1, out byte[] passwordHash1, out byte[] passwordSalt1);

            // Assert
            Assert.NotEqual(passwordHash0, passwordHash1);
            Assert.NotEqual(passwordSalt0, passwordSalt1);
        }
    }
}
