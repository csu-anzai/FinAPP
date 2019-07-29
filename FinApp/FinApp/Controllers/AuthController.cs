using BLL.Services.IServices;
using DAL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService userService)
        {
            _authService = userService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserLoginDTO user)
        {
            var userData = await _authService.SignInAsync(user);

            if (userData == null)
                return NotFound();

            // TODO: sending an access token to the front-end
            // A random jwt token below
            return Ok(new { token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(User user)
        {
            var newData = await _authService.SignUpAsync(user);

            if (newData == null)
                return Unauthorized();

            return Ok(newData);
        }

    }
}
