using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class PersonalDataListRepository : IPersonalDataListRepository
    {
        private readonly AppDbContext _appDbContext;

        public PersonalDataListRepository(AppDbContext appDbContext)
        {
            _appDbContext.Database.EnsureCreated();
            _appDbContext = appDbContext;
        }

        public int CreatePersonalDataList(UserDataList PersonalDataList)
        {
            throw new NotImplementedException();
        }

        public UserDataList GetPersonalDataList(int Id)
        {
            throw new NotImplementedException();
        }

        public List<UserDataList> GetPersonalDataListByUserId(Guid UserId)
        {
            throw new NotImplementedException();
        }
        public void DeletePersonalDataList(int Id)
        {
            throw new NotImplementedException();
        }
        public void UpdatePersonalDataList(UserDataList PersonalDataList)
        {
            throw new NotImplementedException();
        }
    }
}
