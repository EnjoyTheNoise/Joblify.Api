using System.Threading.Tasks;
using Joblify.Core.Login;
using Joblify.Core.Login.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Joblify.Core.Api.Login
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var exists = await _loginService.CheckIfUserExists(loginDto);
            if (exists)
            {
                return Ok();
            }
            await _loginService.RegisterUser(loginDto);
            return Created("", loginDto);
        }
    }
}
