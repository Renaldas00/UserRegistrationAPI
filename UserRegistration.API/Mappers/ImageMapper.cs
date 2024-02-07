using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.DTOS.Responses;
using UserRegistration.API.Mappers.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;

namespace UserRegistration.API.Mappers
{
    public class ImageMapper : IImageMapper
    {
        public ImageResultDTO Map(DAL.Entities.Image entity)
        {
            return new ImageResultDTO
            {
                Id = entity.Id,
                ImageName = entity.ImageName,
            };
        }

        public List<ImageResultDTO> Map(IEnumerable<DAL.Entities.Image> entities)
        {
            return entities.Select(Map).ToList();
        }

        public DAL.Entities.Image Map(UploadImageRequestDTO dto, int todoItemId, int targetWidth, int targetHeight)
        {
            using var stream = dto.Image.OpenReadStream();
            using var image = SixLabors.ImageSharp.Image.Load(stream);

            using (var resizedImage = image.Clone(x => x.Resize(targetWidth, targetHeight)))
            {
                using (var resizedStream = new MemoryStream())
                {
                    // Use a specific image format to encode the resized image
                    var encoder = new PngEncoder(); // You can use other formats like JpegEncoder if needed
                    resizedImage.Save(resizedStream, encoder);

                    return new DAL.Entities.Image
                    {
                        ImageName = dto.ImageName!,
                        UserDataItemId = todoItemId,
                        Content = resizedStream.ToArray(),
                        Size = (int)resizedStream.Length,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                }
            }
        }
            public void ProjectTo(UpdateImageRequestDTO from, DAL.Entities.Image to)
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
