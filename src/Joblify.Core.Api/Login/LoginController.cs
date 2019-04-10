using Joblify.Core.Login;
using Joblify.Core.Login.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Joblify.Core.Api.Login
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;

        public LoginController()
        {
            _loginService = new LoginService();
        }

        [HttpGet]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
