using Microsoft.EntityFrameworkCore;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class PhotoListRepository : IPhotoListRepository
    {
        private readonly AppDbContext _appDbContext;

        public PhotoListRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int AddPhoto(Photo photo)
        {
            _appDbContext.Photo.Add(photo);
            _appDbContext.SaveChanges();
            return photo.Id;
        }
        public void UpdatePhoto(Photo photo)
        {
            _appDbContext.Photo.Update(photo);
            _appDbContext.SaveChanges();
        }
        public void DeletePhoto(Photo photo)
        {
            _appDbContext.Photo.Remove(photo);
            _appDbContext.SaveChanges();
        }

        public Photo GetUserPhoto(int id)
        {
            return _appDbContext.Photo.Include(i => i.UserDataListItem).FirstOrDefault(i => i.Id == id);
        }
    }
}
