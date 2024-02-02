using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class LocationListRepository : ILocationListRepository
    {
        private readonly AppDbContext _appDbContext;

        public LocationListRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int CreateLocationList(LocationList LocationList)
        {
            throw new NotImplementedException();
        }
        public LocationList GetLocationList(int id)
        {
            throw new NotImplementedException();
        }

        public List<LocationList> GetLocationListByUserId(int UserId)
        {
            throw new NotImplementedException();
        }

        public void DeleteLocationList(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateLocationList(LocationList LocationList)
        {
            throw new NotImplementedException();
        }
    }
}
