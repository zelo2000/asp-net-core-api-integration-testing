using Microsoft.AspNetCore.Mvc;
using OZ.UserApi.Services.UserImages;

namespace OZ.UserApi.WebApi.Controllers
{
    /// <summary>
    /// User image controller
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/user/image")]
    [ApiVersion("1.0")]
    public class UserImageController : ControllerBase
    {
        private readonly IUserImageService _userImageService;

        public UserImageController(IUserImageService userImageService)
        {
            _userImageService = userImageService;
        }

        /// <summary>
        /// Upload image
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="file">File</param>
        /// <response code="200">File name</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> UploadImage(Guid userId, [FromForm] IFormFile file)
        {
            var fileName = await _userImageService.UploadImage(userId, file);
            return Ok(fileName);
        }

        /// <summary>
        /// Delete image
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <response code="204">When image deleted</response>
        /// <response code="404">The user is not found</response>
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteImage(Guid userId)
        {
            await _userImageService.DeleteImage(userId);
            return NoContent();
        }
    }
}
