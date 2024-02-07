using Microsoft.EntityFrameworkCore;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        private readonly AppDbContext _appDbContext;

        public ImageRepository(AppDbContext context) : base(context)
        {
        }

        public Image GetUserPhoto(int id)
        {
            return _appDbContext.Image.Include(i => i.UserDataItem).FirstOrDefault(i => i.Id == id);
        }
    }
}
