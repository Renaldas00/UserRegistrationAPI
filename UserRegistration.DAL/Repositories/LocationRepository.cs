using Microsoft.EntityFrameworkCore;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(AppDbContext context) : base(context)
        {
        }

        public Location? Get(int id)
        {
            return _appDbContext.Location.Include(i => i.UserLocation).FirstOrDefault(i => i.Id == id);
        }
    }
}
