using UserRegistration.API.DTOS.Responses;
using UserRegistration.API.DTOS.Requests;
using UserRegistration.DAL.Entities;

namespace UserRegistration.API.Mappers.Interfaces
{
    public interface IImageMapper
    {
        /// <summary>
        /// Maps an UploadImageRequestDTO to an Image entity with the specified TodoItem ID,
        /// resizing the image to the specified target width and height.
        /// </summary>
        /// <param name="dto">The UploadImageRequestDTO containing the image data.</param>
        /// <param name="todoItemId">The ID of the associated TodoItem.</param>
        /// <param name="targetWidth">The target width to resize the image.</param>
        /// <param name="targetHeight">The target height to resize the image.</param>
        /// <returns>The Image entity mapped from the UploadImageRequestDTO.</returns>
        Image Map(UploadImageRequestDTO dto, int todoItemId, int targetWidth, int targetHeight);

        /// <summary>
        /// Maps a collection of Image entities to a collection of ImageResultDTOs.
        /// </summary>
        /// <param name="entities">The collection of Image entities to map.</param>
        /// <returns>A collection of ImageResultDTOs mapped from the Image entities.</returns>
        List<ImageResultDTO> Map(IEnumerable<Image> entities);

        /// <summary>
        /// Projects properties from an UpdateImageRequestDTO to an existing Image entity.
        /// </summary>
        /// <param name="from">The UpdateImageRequestDTO containing the updated image data.</param>
        /// <param name="to">The existing Image entity to update.</param>
        void ProjectTo(UpdateImageRequestDTO from, Image to);

    }
}
