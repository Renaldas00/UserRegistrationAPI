using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class PersonalDataListRepository : IPersonalDataListRepository
    {
        private readonly AppDbContext _appDbContext;

        public PersonalDataListRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int CreatePersonalDataList(PersonalDataList PersonalDataList)
        {
            throw new NotImplementedException();
        }

        public PersonalDataList GetPersonalDataList(int Id)
        {
            throw new NotImplementedException();
        }

        public List<PersonalDataList> GetPersonalDataListByUserId(int UserId)
        {
            throw new NotImplementedException();
        }
        public void DeletePersonalDataList(int Id)
        {
            throw new NotImplementedException();
        }
        public void UpdatePersonalDataList(PersonalDataList PersonalDataList)
        {
            throw new NotImplementedException();
        }
    }
}
