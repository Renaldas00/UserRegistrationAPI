using Microsoft.EntityFrameworkCore;
using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class UserDataListRepository : IUserDataListRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserDataListRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int CreateUserDataList(UserDataList userDataList)
        {
            _appDbContext.UserDataList.Add(userDataList);
            _appDbContext.SaveChanges();
            return userDataList.Id;
        }

        public UserDataList GetUserDataList(int id)
        {
            return _appDbContext.UserDataList.Include(i => i.Photo).FirstOrDefault(i => i.Id == id);

        }
        public void DeleteUserDataList(UserDataList userDataList)
        {
            _appDbContext.UserDataList.Remove(userDataList);
            _appDbContext.SaveChanges();
        }
        public void UpdateUserDataList(UserDataList userDataList)
        {
            _appDbContext.UserDataList.Update(userDataList);
            _appDbContext.SaveChanges();
        }
    }
}
