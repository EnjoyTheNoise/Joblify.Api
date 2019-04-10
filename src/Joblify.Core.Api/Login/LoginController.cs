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
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var exists = _loginService.CheckIfUserExists(loginDto);
            if (exists)
            {
                return Ok();
            }
            _loginService.RegisterUser(loginDto);
            return Created("", loginDto);
        }
    }
}
