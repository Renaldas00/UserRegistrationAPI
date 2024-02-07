using Microsoft.EntityFrameworkCore;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly AppDbContext _appDbContext;

        public ImageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int AddPhoto(Image photo)
        {
            _appDbContext.Image.Add(photo);
            _appDbContext.SaveChanges();
            return photo.Id;
        }
        public void UpdatePhoto(Image photo)
        {
            _appDbContext.Image.Update(photo);
            _appDbContext.SaveChanges();
        }
        public void DeletePhoto(Image photo)
        {
            _appDbContext.Image.Remove(photo);
            _appDbContext.SaveChanges();
        }

        public Image GetUserPhoto(int id)
        {
            return _appDbContext.Image.Include(i => i.UserDataItem).FirstOrDefault(i => i.Id == id);
        }
    }
}
