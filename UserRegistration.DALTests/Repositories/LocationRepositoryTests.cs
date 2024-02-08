using Microsoft.EntityFrameworkCore;
using System.Text;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories;
using UserRegistration.DAL;
using Xunit;
using System.Diagnostics;

namespace UserRegistration.DALTests.Repositories
{
    public class LocationRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly LocationRepository _locationRepository;

        public LocationRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid())
                .Options;
            _context = new AppDbContext(options);
            _locationRepository = new LocationRepository(_context);
        }

        [Fact]
        public void GetAll_NoLocations_ReturnsEmpty()
        {
            // Act
            var result = _locationRepository.GetAll();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAll_IncludeUserData_ReturnsLocationWithUserData()
        {
            // Arrange
            var userData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserData1",
                EmailAddres = "dasdsa@gmail.com",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                CreatedAt = DateTime.Now,
            };
            var location = new Location
            {
                Id = 1,
                City = "Location1",
                Country = "Country1",
                Street = "Street1",
                HouseNumber = "house1",
                ApartmentNumber = "",
                UserLocationId = userData.Id,
                UserLocation = userData // Assign user data to location
            };
            _context.UserData.Add(userData);
            _context.Location.Add(location);
            _context.SaveChanges();

            // Act
            var result = _locationRepository.GetAll(i => i.UserLocation).First();

            // Assert
            Assert.NotNull(result.UserLocation);
            Assert.Equal("UserData1", result.UserLocation.LastName);
        }

        [Fact]
        public void Get_ValidId_ReturnsCorrectLocation()
        {
            // Arrange
            var userData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserData1",
                EmailAddres = "dasdsa@gmail.com",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                CreatedAt = DateTime.Now,
            };
            var location = new Location
            {
                Id = 1,
                City = "Location1",
                Country = "Country1",
                Street = "Street1",
                HouseNumber = "house1",
                ApartmentNumber = "",
                UserLocationId = userData.Id,
                UserLocation = userData // Assign user data to location
            };
            _context.UserData.Add(userData);
            _context.Location.Add(location);
            _context.SaveChanges();

            // Act
            var result = _locationRepository.Get(location.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(location.City, result.City);
            Assert.Equal(location.Country, result.Country);
        }

        [Fact]
        public void Get_InvalidId_ReturnsNull()
        {
            // Act
            var result = _locationRepository.Get(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Add_ValidLocation_ReturnsNonNullId()
        {
            // Arrange
            var location = new Location
            {
                City = "Location1",
                Country = "Country1",
                Street = "Street1",
                HouseNumber = "house1",
                ApartmentNumber = "",
                UserLocationId = 1
            };

            // Act
            _locationRepository.Add(location);

            // Assert
            Assert.NotEqual(0, location.Id);
        }

        [Fact]
        public void Update_ValidLocation_ChangesAreSaved()
        {
            // Arrange
            var location = new Location
            {
                Id = 1,
                City = "Location1",
                Country = "Country1",
                Street = "Street1",
                HouseNumber = "house1",
                ApartmentNumber = "",
                UserLocationId = 1
            };
            _context.Location.Add(location);
            _context.SaveChanges();

            // Act
            location.City = "NewLocationName";
            _locationRepository.Update(location);

            // Assert
            Assert.Equal("NewLocationName", _context.Location.Find(location.Id)?.City);
        }

        [Fact]
        public void Update_LocationNotInDatabase_ThrowsException()
        {
            // Arrange
            var location = new Location
            {
                Id = 1,
                City = "Location1",
                Country = "Country1",
                Street = "Street1",
                HouseNumber = "house1",
                ApartmentNumber = "",
                UserLocationId = 1
            };

            // Act & Assert
            Assert.Throws<DbUpdateConcurrencyException>(() => _locationRepository.Update(location));
        }

        [Fact]
        public void Delete_ValidId_LocationDoesNotExist()
        {
            // Arrange
            var location = new Location
            {
                Id = 1,
                City = "Location1",
                Country = "Country1",
                Street = "Street1",
                HouseNumber = "house1",
                ApartmentNumber = "",
                UserLocationId = 1
            };
            _context.Location.Add(location);
            _context.SaveChanges();

            // Act
            _locationRepository.Delete(location);

            // Assert
            Assert.Null(_context.Location.Find(location.Id));
        }

        [Fact]
        public void Delete_LocationNotInDatabase_ThrowsException()
        {
            // Arrange
            var location = new Location
            {
                Id = 1,
                City = "Location1",
                Country = "Country1",
                Street = "Street1",
                HouseNumber = "house1",
                ApartmentNumber = "",
                UserLocationId = 1
            };

            // Act & Assert
            Assert.Throws<DbUpdateConcurrencyException>(() => _locationRepository.Delete(location));
        }

        [Fact]
        public void Delete_ValidId_LocationDoesNotExistButUserDataExist()
        {
            // Arrange
            var userData = new UserData
            {
                Id = 1,
                FirstName = "Other",
                LastName = "UserData1",
                EmailAddres = "dasdsa@gmail.com",
                SocialSecurityCode = "123456789",
                PhoneNumber = "+37069999999",
                CreatedAt = DateTime.Now,
            };
            var location = new Location
            {
                Id = 1,
                City = "Location1",
                Country = "Country1",
                Street = "Street1",
                HouseNumber = "house1",
                ApartmentNumber = "",
                UserLocationId = userData.Id,
                UserLocation = userData // Assign user data to location
            };
            _context.UserData.Add(userData);
            _context.Location.Add(location);
            _context.SaveChanges();

            // Act
            _locationRepository.Delete(location);

            // Assert
            Assert.Null(_context.Location.Find(location.Id));
            Assert.NotNull(_context.UserData.Find(userData.Id));
        }

        //    private readonly AppDbContext _context;
        //    private readonly LocationRepository _userDataRepository;

        //    public LocationRepositoryTests()
        //    {
        //        var options = new DbContextOptionsBuilder<AppDbContext>()
        //            .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid())
        //            .Options;
        //        _context = new AppDbContext(options);
        //        _userDataRepository = new LocationRepository(_context);
        //    }

        //    [Fact]
        //    public void GetAll_NoLocations_ReturnsEmpty()
        //    {
        //        // Act
        //        var result = _userDataRepository.GetAll();

        //        // Assert
        //        Assert.Empty(result);
        //    }

        //    [Fact]
        //    public void GetAll_IncludeUserData_ReturnsLocationWithUserData()
        //    {
        //        // Arrange
        //        var userData = new UserData
        //        {
        //            Id = 1,
        //            FirstName = "Other",
        //            LastName = "UserData1",
        //            EmailAddres = "dasdsa@gmail.com",
        //            SocialSecurityCode = "123456789",
        //            PhoneNumber = "+37069999999",
        //            CreatedAt = DateTime.Now,
        //        };
        //        var location1 = new Location
        //        {
        //            Id = 1,
        //            City = "Location1",
        //            Country = "Country1",
        //            Street = "Street1",
        //            HouseNumber = "house1",
        //            ApartmentNumber = "",
        //            UserLocationId = 1
        //        };
        //        _context.UserData.Add(userData);
        //        _context.Location.Add(location1);
        //        _context.SaveChanges();

        //        // Act
        //        var result = _userDataRepository.GetAll(i => i.UserLocation).First();

        //        // Assert
        //        Assert.NotNull(result.UserLocation);
        //        Assert.Equal("UserData1", result.UserLocation.LastName);
        //    }
        //    [Fact]
        //    public void Get_ValidId_ReturnsCorrectLocation()
        //    {
        //        // Arrange
        //        var userData = new UserData
        //        {
        //            Id = 1,
        //            FirstName = "Other",
        //            LastName = "UserData1",
        //            EmailAddres = "dasdsa@gmail.com",
        //            SocialSecurityCode = "123456789",
        //            PhoneNumber = "+37069999999",
        //            CreatedAt = DateTime.Now,
        //        };
        //        var location = new Location
        //        {
        //            Id = 1,
        //            City = "Location1",
        //            Country = "Country1",
        //            Street = "Street1",
        //            HouseNumber = "house1",
        //            ApartmentNumber = "",
        //            UserLocationId = 1
        //        };
        //        _context.UserData.Add(userData);
        //        _context.Location.Add(location);
        //        _context.SaveChanges();

        //        // Act
        //        var result = _userDataRepository.Get(userData.Id);

        //        // Assert
        //        Assert.NotNull(result);
        //        Assert.Equal(userData.Location, result.UserLocation);
        //    }
        //    [Fact]
        //    public void Get_InvalidId_ReturnsNull()
        //    {
        //        // Act
        //        var result = _userDataRepository.Get(1);

        //        // Assert
        //        Assert.Null(result);
        //    }
        //    [Fact]
        //    public void Add_ValidLocation_ReturnsNonNullId()
        //    {
        //        // Arrange
        //        var location = new Location
        //        {
        //            LocationName = "Location1",
        //            Content = Encoding.UTF8.GetBytes("Content1"),
        //            UserLocationId = 1
        //        };

        //        // Act
        //        _userDataRepository.Add(userData);

        //        // Assert
        //        Assert.NotEqual(0, userData.Id);
        //    }
        //    [Fact]
        //    public void Update_ValidLocation_ChangesAreSaved()
        //    {
        //        // Arrange
        //        var userData = new UserData
        //        {
        //            Id = 1,
        //            FirstName = "Other",
        //            LastName = "UserData1",
        //            EmailAddres = "dasdsa@gmail.com",
        //            SocialSecurityCode = "123456789",
        //            PhoneNumber = "+37069999999",
        //            CreatedAt = DateTime.Now,

        //        };
        //        var userData = new Location
        //        {
        //            Id = 1,
        //            LocationName = "Location1",
        //            Content = Encoding.UTF8.GetBytes("Content1"),
        //            UserLocationId = 1
        //        };
        //        _context.UserData.Add(userData);
        //        _context.Location.Add(userData);
        //        _context.SaveChanges();

        //        // Act
        //        userData.LocationName = "NewLocationName";
        //        _userDataRepository.Update(userData);

        //        // Assert
        //        var result = _userDataRepository.Get(userData.Id);
        //        Assert.Equal("NewLocationName", result.LocationName);
        //    }
        //    [Fact]
        //    public void Update_LocationNotInDatabase_ThrowsException()
        //    {
        //        // Arrange
        //        var userData = new Location
        //        {
        //            Id = 1,
        //            LocationName = "Location1",
        //            Content = Encoding.UTF8.GetBytes("Content1"),
        //            UserLocationId = 1
        //        };

        //        // Act & Assert
        //        Assert.Throws<DbUpdateConcurrencyException>(() => _userDataRepository.Update(userData));
        //    }
        //    [Fact]
        //    public void Delete_ValidId_LocationDoesNotExist()
        //    {
        //        // Arrange
        //        var userData = new UserData
        //        {
        //            Id = 1,
        //            FirstName = "Other",
        //            LastName = "UserData1",
        //            EmailAddres = "dasdsa@gmail.com",
        //            SocialSecurityCode = "123456789",
        //            PhoneNumber = "+37069999999",
        //            CreatedAt = DateTime.Now,
        //        };
        //        var userData = new Location
        //        {
        //            Id = 1,
        //            LocationName = "Location1",

        //            Content = Encoding.UTF8.GetBytes("Content1"),
        //            UserLocationId = 1
        //        };
        //        _context.UserData.Add(userData);
        //        _context.Location.Add(userData);
        //        _context.SaveChanges();

        //        // Act
        //        _userDataRepository.Delete(userData);

        //        // Assert
        //        Assert.Null(_context.Location.Find(userData.Id));
        //    }
        //    [Fact]
        //    public void Delete_LocationNotInDatabase_ThrowsException()
        //    {
        //        // Arrange
        //        var userData = new Location
        //        {
        //            Id = 1,
        //            LocationName = "Location1",
        //            Content = Encoding.UTF8.GetBytes("Content1"),
        //            UserLocationId = 1
        //        };

        //        // Act & Assert
        //        Assert.Throws<DbUpdateConcurrencyException>(() => _userDataRepository.Delete(userData));
        //    }
        //    [Fact]
        //    public void Delete_ValidId_LocationDoesNotExistButUserDataExist()
        //    {
        //        // Arrange
        //        var userData = new UserData
        //        {
        //            Id = 1,
        //            FirstName = "Other",
        //            LastName = "UserData1",
        //            EmailAddres = "dasdsa@gmail.com",
        //            SocialSecurityCode = "123456789",
        //            PhoneNumber = "+37069999999",
        //            CreatedAt = DateTime.Now,
        //        };
        //        var userData = new Location
        //        {
        //            Id = 1,
        //            LocationName = "Location1",
        //            Content = Encoding.UTF8.GetBytes("Content1"),
        //            UserLocationId = 1
        //        };
        //        _context.UserData.Add(userData);
        //        _context.Location.Add(userData);
        //        _context.SaveChanges();

        //        // Act
        //        _userDataRepository.Delete(userData);

        //        // Assert
        //        Assert.Null(_context.Location.Find(userData.Id));
        //        Assert.NotNull(_context.UserData.Find(userData.Id));

        //    }
    }
}
