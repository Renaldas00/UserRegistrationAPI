using UserRegistration.API.DTOS.Responses;
using UserRegistration.API.DTOS.Requests;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers.Interfaces
{
    public interface IImageMapper
    {
        Image Map(UploadImageRequestDTO dto, int todoItemId, int targetWidth, int targetHeight);
        List<ImageResultDTO> Map(IEnumerable<Image> entities);

        void ProjectTo(UpdateImageRequestDTO from, Image to);
    }
}
