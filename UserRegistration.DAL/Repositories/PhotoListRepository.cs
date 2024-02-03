using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class PhotoListRepository : IPhotoListRepository
    {
        private readonly AppDbContext _appDbContext;

        public PhotoListRepository(AppDbContext appDbContext)
        {
            _appDbContext.Database.EnsureCreated();
            _appDbContext = appDbContext;
        }

        public int AddPhoto(Photo Photo)
        {
            throw new NotImplementedException();
        }
        public void UpdatePhoto(Photo Photo)
        {
            throw new NotImplementedException();
        }
        public void DeletePhoto(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
