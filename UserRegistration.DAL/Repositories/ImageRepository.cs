using Microsoft.EntityFrameworkCore;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(AppDbContext context) : base(context)
        {
        }

        public Image Get(int id)
        {
            return _appDbContext.Image.Include(i => i.UserDataItem).FirstOrDefault(i => i.Id == id);
        }
    }
}
