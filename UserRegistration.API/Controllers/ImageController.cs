﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using UserRegistration.API.DTOS.Requests;
using UserRegistration.API.DTOS.Responses;
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
        private readonly IPhotoListRepository _imageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoMapper _mapper;
        private readonly IUserDataListRepository _todoRepository;

        private readonly Guid _userId;
        public ImageController(ILogger<ImageController> logger,
            IPhotoListRepository imageRepository,
            IHttpContextAccessor httpContextAccessor,
            IPhotoMapper mapper,
            IUserDataListRepository todoRepository)
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
            var entity = _imageRepository.GetUserPhoto(id);
            if (entity == null)
            {
                _logger.LogInformation($"Image {id} not found for user {_userId}");
                return NotFound();
            }
            if (entity.UserPhoto.AccountId != _userId)
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
            var todoEntity = _todoRepository.GetUserDataList(todoItemId);
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

            var image = _mapper.Map(req, todoItemId);
            _imageRepository.AddPhoto(image);

            return Created(nameof(Get), new { id = image.Id });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Deleting image {id} for user {_userId}");
            var entity = _imageRepository.GetUserPhoto(id);
            if (entity == null)
            {
                _logger.LogInformation($"Image {id} not found for user {_userId}");
                return NotFound();
            }
            if (entity.UserPhoto.AccountId != _userId)
            {
                _logger.LogInformation($"Image {id} is forbidden for user {_userId}");
                return Forbid();
            }
            _imageRepository.DeletePhoto(entity);
            return NoContent();
        }
    }
}
