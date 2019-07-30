using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AuthController(IAuthService userService, IMapper mapper)
        {
            _authService = userService;
            _mapper = mapper;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserLoginDTO userDto)
        {
            var user = await _authService.SignInAsync(userDto);

            if (user == null)
                return NotFound();

            // TODO: sending an access token to the front-end
            // A random jwt token below
            return Ok(new { token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserRegistrationDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            user.RoleId = 1;

            var newUser = await _authService.SignUpAsync(user);

            if (newUser == null)
                return Unauthorized();

            return Ok(newUser);
        }

    }
}
