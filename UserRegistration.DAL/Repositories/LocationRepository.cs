using Microsoft.EntityFrameworkCore;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _appDbContext;

        public LocationRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int CreateLocationList(Location locationList)
        {
            _appDbContext.Location.Add(locationList);
            _appDbContext.SaveChanges();
            return locationList.Id;
        }
        public Location GetLocationListById(int id)
        {
            return _appDbContext.Location.Include(i => i.UserLocation).FirstOrDefault(i => i.Id == id);
        }

        public void DeleteLocationList(Location locationList)
        {
            _appDbContext.Location.Remove(locationList);
            _appDbContext.SaveChanges();
        }

        public void UpdateLocationList(Location locationList)
        {
            _appDbContext.Location.Update(locationList);
            _appDbContext.SaveChanges();
        }
    }
}
