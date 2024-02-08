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
        private readonly IUserDataRepository _userDataRepository;

        private readonly Guid _userId;
        public ImageController(ILogger<ImageController> logger,
            IImageRepository imageRepository,
            IHttpContextAccessor httpContextAccessor,
            IImageMapper mapper,
            IUserDataRepository userDataRepository)
        {
            _logger = logger;
            _imageRepository = imageRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userId = new Guid(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            _userDataRepository = userDataRepository;
        }

        /// <summary>
        /// Get Image For User
        /// </summary>
        /// <param name="id">Id To Search For</param>
        /// <response code="200">Image Content</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// Upload Image For User
        /// </summary>
        /// <param name="req">Image And Its Data</param>
        /// <response code="200">Image Id</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpPost]
        [Route("/api/userdata/{userDataItemId}/[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Post([FromRoute] int userDataItemId, [FromForm] UploadImageRequestDTO req)
        {
            _logger.LogInformation($"Creating image for user {_userId}");
            var userDataEntity = _userDataRepository.Get(userDataItemId);
            if (userDataEntity == null)
            {
                _logger.LogInformation($"UserData with id {userDataItemId} for user {_userId} not found");
                return NotFound("UserData item not found");
            }
            if (userDataEntity.AccountId != _userId)
            {
                _logger.LogInformation($"UserData with id {userDataItemId} for user {_userId} is forbidden");
                return Forbid();
            }

            var image = _mapper.Map(req, userDataItemId, 200,200);
            _imageRepository.Add(image);

            return Created(nameof(Get), new { id = image.Id });
        }
        /// <summary>
        /// Deletes User Image
        /// </summary>
        /// <param name="id">Image ID</param>
        /// <response code="204">No Content</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// Update User Image
        /// </summary>
        /// <param name="id">ID To Search For</param>
        /// <param name="req">Image Data</param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(int id, [FromForm] UpdateImageRequestDTO req)

        {
            _logger.LogInformation($"Updating user image {id} for user {_userId}");
            var entity = _imageRepository.Get(id);
            if (entity == null)
            {
                _logger.LogInformation($"Updating user image {id} for user {_userId} not found");
                return NotFound();
            }
            if (entity.UserDataItem.AccountId != _userId)
            {
                _logger.LogInformation($"Updating user image {id} for user {_userId} is forbidden");
                return Forbid();
            }
            _mapper.ProjectTo(req, entity,200,200);
            _imageRepository.Update(entity);
            return NoContent();
        }

        /// <summary>
        /// Download Image
        /// </summary>
        /// <param name="id">Image ID</param>
        /// <response code="200">Image Link</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">System error</response>
        [HttpGet("download/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
