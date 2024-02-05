using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.DTOS.Responses;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers.Interfaces
{
    public interface IUserDataListMapper
    {
        UserDataListResultDTO Map(UserDataList entity);
        List<UserDataListResultDTO> Map(IEnumerable<UserDataList> entities);
        UserDataList Map(UserDataListRequestDTO dto);

        void ProjectTo(UpdateFirstNameUserDataListRequestDTO from, UserDataList to);

        void ProjectTo(UpdateLastNameUserDataListRequestDTO from, UserDataList to);

        void ProjectTo(UpdateEmailAddressUserDataListRequestDTO from, UserDataList to);

        void ProjectTo(UpdateSocSecCodeUserDataListRequestDTO from, UserDataList to);

        void ProjectTo(UpdatePhoneNumberUserDataListRequestDTO from, UserDataList to);
    }
}
