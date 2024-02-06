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
            using var stream = new MemoryStream();
            dto.Image.CopyTo(stream);
            var imageBytes = stream.ToArray();
            return new Photo
            {
                ImageName = dto.ImageName!,
                UserDataListItemId = todoItemId,
                Content = imageBytes,
                Size = imageBytes.Length,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow

            };
        }

        public void ProjectTo(UpdateImageRequestDTO from, Photo to)
        {
            using var stream = new MemoryStream();
            from.Image.CopyTo(stream);
            var imageBytes = stream.ToArray();

            to.ImageName = from.ImageName!;
            to.Content = imageBytes;
            to.UpdatedAt = DateTime.UtcNow; ;
        }
    }
}
