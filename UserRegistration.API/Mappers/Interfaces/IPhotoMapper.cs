using UserRegistration.API.DTOS.Responses;
using UserRegistration.API.DTOS.Requests;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers.Interfaces
{
    public interface IPhotoMapper
    {
        Photo Map(UploadImageRequestDTO dto, int todoItemId);
        List<ImageResultDTO> Map(IEnumerable<Photo> entities);

        void ProjectTo(UpdateImageRequestDTO from, Photo to);
    }
}
