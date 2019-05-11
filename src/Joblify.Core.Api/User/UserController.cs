using System.Threading.Tasks;
using Joblify.Core.Api.Infrastructure.ActionFilterAttributes;
using Joblify.Core.User;
using Joblify.Core.User.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Joblify.Core.Api.User
{
    [Route("api/login")]
    public class UserController : Controller
    {
        private readonly IUserService _loginService;

        public UserController(IUserService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> CheckIfEmailExists(string email)
        {
            var result = await _loginService.CheckIfUserExists(email);

            return Ok(result);
        }

        [HttpPost("saveProfile")]
        [ValidateModel]
        public async Task<IActionResult> SaveProfile([FromBody] EditProfileDto dto)
        {
            var result = await _loginService.SaveProfile(dto);

            if (result == null)
            {
                return BadRequest();
            }

            return Created("", result);
        }

        [HttpDelete("deleteProfile")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var userFromRepo = await _loginService.GetUser(email);
            if (userFromRepo == null)
            {
                return NotFound();
            }

            await _loginService.DeleteUser(userFromRepo);

            return NoContent();
        }
    }
}
