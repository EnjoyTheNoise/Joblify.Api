using System.Threading.Tasks;
using Joblify.Core.Api.Infrastructure.ActionFilterAttributes;
using Joblify.Core.User;
using Joblify.Core.User.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Joblify.Core.Api.User
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService loginService)
        {
            _userService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string email)
        {
            var result = await _userService.GetUser(email);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> CheckIfEmailExists(string email)
        {
            var result = await _userService.CheckIfUserExists(email);
            return Ok(result);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateProfile([FromBody] AddUserDto dto)
        {
            var result = await _userService.CreateUser(dto);

            if (result == null)
            {
                return BadRequest();
            }

            return Created("", result);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto)
        {
            var result = await _userService.UpdateUser(dto);

            if (result == null)
            {
                return BadRequest();
            }

            return Created("", result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var userEntityForDelete = await _userService.GetUser(email);
            if (userEntityForDelete == null)
            {
                return NotFound();
            }

            if (userEntityForDelete.IsDeleted)
            {
                return NotFound();
            }

            await _userService.DeleteUser(userEntityForDelete);
            return NoContent();
        }
    }
}
