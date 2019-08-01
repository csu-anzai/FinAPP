using AutoMapper;
using BLL.Services.IServices;
using DAL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserRegistrationDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var isExist = await _userService.IsExist(user.Email);

            if (isExist)
                return BadRequest();

            user.RoleId = 1;

            var newUser = await _userService.CreateUserAsync(user);

            if (newUser == null)
                return Unauthorized();

            return Ok(newUser);
        }
    }
}
