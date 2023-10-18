using Microsoft.AspNetCore.Mvc;
using OZ.UserApi.Services.Auth;
using OZ.UserApi.Services.Auth.Models;
using OZ.UserApi.Services.Users.Models;

namespace OZ.UserApi.WebApi.Controllers
{
    /// <summary>
    /// Auth controller
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/user")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Log in user
        /// </summary>
        /// <param name="payload">Login and password</param>
        /// <response code="200">Returns user</response>
        /// <response code="404">The user is not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<User>>> LogIn([FromBody] AuthPayload payload)
        {
            var user = await _authService.LogIn(payload.Email, payload.Password);
            return Ok(user);
        }
    }
}
