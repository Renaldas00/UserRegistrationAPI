using UserRegistration.API.DTOS.Requests;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers.Interfaces
{
    public interface IUserDataListMapper
    {
        UserDataListRequestDTO Map(UserDataList entity);
        List<UserDataListRequestDTO> Map(IEnumerable<UserDataList> entities);
        UserDataList Map(UserDataListRequestDTO dto);
        void ProjectTo(UserDataListRequestDTO from, UserDataList to);
    }
}
