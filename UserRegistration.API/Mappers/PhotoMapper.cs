using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.DTOS.Responses;
using UserRegistration.API.Mappers.Interfaces;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers
{
    public class PhotoMapper : IPhotoMapper
    {
        public ImageResultDTO Map(Photo entity)
        {
            return new ImageResultDTO
            {
                Id = entity.Id,
                ImageName = entity.ImageName,
            };
        }

        public List<ImageResultDTO> Map(IEnumerable<Photo> entities)
        {
            return entities.Select(Map).ToList();
        }


        public Photo Map(UploadImageRequestDTO dto, int todoItemId)
        {
            //using var stream = new MemoryStream();
            //dto.Image.CopyTo(stream);
            //var imageBytes = stream.ToArray();
            return new Photo
            {
                ImageName = dto.ImageName!,
                UserPhotoId = todoItemId,
                //Content = imageBytes,
                Content = dto.Content,
                Size = dto.Size
            };
        }

      
    }
}
