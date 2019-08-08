using AutoMapper;
using BLL.Services.IServices;
using DAL.DTOs;
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

        public AuthController(IAuthService userService, IMapper mapper)
        {
            _authService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDTO userDto)
        {
            var token = await _authService.SignInAsync(userDto);

            if (token == null)
                return BadRequest(new { message = "Credentials are invalid" });

            return Ok(new { token = token.AccessToken });
        }

        [HttpPost]
        [Route("signingoogle")]
        public async Task<IActionResult> GoogleSignIn(GoogleUserDTO userDto)
        {
            var token = await _authService.GoogleSignInAsync(userDto.Email);

            if (token == null)
                return StatusCode(404);

            return Ok(new { token = token.AccessToken });
        }
    }
}
