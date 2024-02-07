using Microsoft.EntityFrameworkCore;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserDataRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int Create(UserData userData)
        {
            _appDbContext.UserData.Add(userData);
            _appDbContext.SaveChanges();
            return userData.Id;
        }

        public UserData Get(int id)
        {
            return _appDbContext.UserData
                .Include(i => i.Image)
                .Include(i => i.Location)
                .FirstOrDefault(i => i.Id == id);
        }
        public void Delete(UserData userData)
        {
            _appDbContext.UserData.Remove(userData);
            _appDbContext.SaveChanges();
        }
        public void Update(UserData userData)
        {
            _appDbContext.UserData.Update(userData);
            _appDbContext.SaveChanges();
        }
    }
}
