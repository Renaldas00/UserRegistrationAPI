using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.DTOS.Responses;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers.Interfaces
{
    public interface IUserDataMapper
    {
        UserDataListResultDTO Map(UserData entity);
        List<UserDataListResultDTO> Map(IEnumerable<UserData> entities);
        UserData Map(UserDataRequestDTO dto);

        void ProjectTo(UpdateFirstNameRequestDTO from, UserData to);

        void ProjectTo(UpdateLastNameRequestDTO from, UserData to);

        void ProjectTo(UpdateEmailAddresRequestDTO from, UserData to);

        void ProjectTo(UpdateSocSecCodeRequestDTO from, UserData to);

        void ProjectTo(UpdatePhoneNumberRequestDTO from, UserData to);
    }
}
