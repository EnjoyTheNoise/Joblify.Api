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

        [HttpGet("{email}")]
        public async Task<IActionResult> CheckIfEmailExists(string email)
        {
            var result = await _userService.CheckIfUserExists(email);

            return Ok(result);
        }

        [HttpPost("saveProfile")]
        [ValidateModel]
        public async Task<IActionResult> SaveProfile([FromBody] EditProfileDto dto)
        {
            var result = await _userService.SaveProfile(dto);

            if (result == null)
            {
                return BadRequest();
            }

            return Created("", result);
        }

        [HttpDelete("deleteProfile")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var userFromRepo = await _userService.GetUser(email);
            if (userFromRepo == null)
            {
                return NotFound();
            }

            await _userService.DeleteUser(userFromRepo);

            return NoContent();
        }
    }
}
