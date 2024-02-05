using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class UserDataListRepository : IUserDataListRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserDataListRepository(AppDbContext appDbContext)
        {
            _appDbContext.Database.EnsureCreated();
            _appDbContext = appDbContext;
        }

        public int CreateUserDataList(UserDataList userDataList)
        {
            _appDbContext.UserDataList.Add(userDataList);
            _appDbContext.SaveChanges();
            return userDataList.Id;
        }

        public UserDataList GetUserDataList(int Id)
        {
            throw new NotImplementedException();
        }

        public List<UserDataList> GetUserDataListByUserId(Guid UserId)
        {
            throw new NotImplementedException();
        }
        public void DeleteUserDataList(int Id)
        {
            throw new NotImplementedException();
        }
        public void UpdateUserDataList(UserDataList UserDataList)
        {
            throw new NotImplementedException();
        }
    }
}
