using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OZ.UserApi.Services.Extension;
using OZ.UserApi.Services.Users;
using OZ.UserApi.Services.Users.Models;

namespace OZ.UserApi.WebApi.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/user")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserPayload> _validator;

        public UserController(IUserService userService, IValidator<UserPayload> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        /// <summary>
        /// Get list of all users
        /// </summary>
        /// <response code="200">Returns the list of users</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <response code="200">Returns the user</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> GetUserById(Guid userId)
        {
            var user = await _userService.GetUserById(userId);
            return Ok(user);
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="payload"></param>
        /// <response code="200">Returns the user</response>
        /// <response code="400">Validation exception</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserPayload payload)
        {
            var result = await _validator.ValidateAsync(payload);
            if (!result.IsValid)
                return BadRequest(result.BuildMessage());

            var user = await _userService.CreateUser(payload);
            return Ok(user);
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="payload"></param>
        /// <response code="200">Returns the user</response>
        /// <response code="400">Validation exceptions or user id is null</response>
        /// <response code="404">The user is not found</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> UpdateUser([FromBody] UserPayload payload)
        {
            var result = await _validator.ValidateAsync(payload);
            if (!result.IsValid)
                return BadRequest(result.BuildMessage());

            var user = await _userService.UpdateUser(payload);
            return Ok(user);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <response code="200">If user deleted</response>
        /// <response code="404">The user is not found</response>
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteUser(Guid userId)
        {
            await _userService.DeleteUser(userId);
            return Ok();
        }
    }
}
