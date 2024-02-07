using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.Mappers.Interfaces;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IImageRepository _imageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageMapper _mapper;
        private readonly IUserDataRepository _todoRepository;

        private readonly Guid _userId;
        public ImageController(ILogger<ImageController> logger,
            IImageRepository imageRepository,
            IHttpContextAccessor httpContextAccessor,
            IImageMapper mapper,
            IUserDataRepository todoRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userId = new Guid(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _todoRepository = todoRepository;
        }

        /// <summary>
        /// get an image for a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Produces(MediaTypeNames.Image.Png)]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Getting image {id} for user {_userId}");
            var entity = _imageRepository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Image {id} not found for user {_userId}");
                return NotFound();
            }
            if (entity.UserDataItem.AccountId != _userId)
            {
                _logger.LogInformation($"Image {id} is forbidden for user {_userId}");
                return Forbid();
            }
            return File(entity.Content, $"image/png");
        }

        /// <summary>
        /// creates an image for a user
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/Todo/{todoItemId}/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Post([FromRoute] int todoItemId, [FromForm] UploadImageRequestDTO req)
        {
            _logger.LogInformation($"Creating image for user {_userId}");
            var todoEntity = _todoRepository.Get(todoItemId);
            if (todoEntity == null)
            {
                _logger.LogInformation($"Todo with id {todoItemId} for user {_userId} not found");
                return NotFound("Todo item not found");
            }
            if (todoEntity.AccountId != _userId)
            {
                _logger.LogInformation($"Todo with id {todoItemId} for user {_userId} is forbidden");
                return Forbid();
            }

            var image = _mapper.Map(req, todoItemId,200,200);
            _imageRepository.Add(image);

            return Created(nameof(Get), new { id = image.Id });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Deleting image {id} for user {_userId}");
            var entity = _imageRepository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Image {id} not found for user {_userId}");
                return NotFound();
            }
            if (entity.UserDataItem.AccountId != _userId)
            {
                _logger.LogInformation($"Image {id} is forbidden for user {_userId}");
                return Forbid();
            }
            _imageRepository.Delete(entity);
            return NoContent();
        }

        /// <summary>
        /// Updates a user data list item first name for the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put(int id, [FromForm] UpdateImageRequestDTO req)

        {
            _logger.LogInformation($"Updating user data list item with id {id} for user {_userId}");
            var entity = _imageRepository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserDataItem.AccountId != _userId)
            {
                _logger.LogInformation($"User data list item with id {id} for user {_userId} is forbidden");
                return Forbid();
            }


            _mapper.ProjectTo(req, entity);
            _imageRepository.Update(entity);
            return NoContent();
        }
        [HttpGet("download/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Produces(MediaTypeNames.Image.Png)]
        public IActionResult DownloadPhoto(int id)
        {
            _logger.LogInformation($"Getting image {id} for user {_userId}");
            var entity = _imageRepository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Image {id} not found for user {_userId}");
                return NotFound();
            }
            if (entity.UserDataItem.AccountId != _userId)
            {
                _logger.LogInformation($"Image {id} is forbidden for user {_userId}");
                return Forbid();
            }
            return File(entity.Content, $"image/png", entity.ImageName);
        }
    }
}
