using System.Threading.Tasks;
using Joblify.Core.Api.Infrastructure.ActionFilterAttributes;
using Joblify.Core.Login;
using Joblify.Core.Login.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Joblify.Core.Api.Login
{
    [Route("api/login")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var exists = await _loginService.CheckIfUserExists(loginDto);
            if (exists)
            {
                return Ok();
            }

            var result = await _loginService.RegisterUser(loginDto);

            if (result == null)
            {
                return BadRequest();
            }

            return Created("", result);
        }
    }
}
