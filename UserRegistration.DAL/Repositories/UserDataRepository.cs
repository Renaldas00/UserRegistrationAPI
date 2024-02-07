using Microsoft.EntityFrameworkCore;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class UserDataRepository : Repository<UserData>, IUserDataRepository
    {
        public UserDataRepository(AppDbContext context) : base(context)
        {
        }
        public UserData? Get(int id)
        {
            return _appDbContext.UserData
                .Include(i => i.Image)
                .Include(i => i.Location)
                .FirstOrDefault(i => i.Id == id);
        }
    }
}
