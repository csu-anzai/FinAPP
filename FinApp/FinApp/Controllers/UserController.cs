using AutoMapper;
using BLL.Services.IServices;
using DAL.DTOs;
using DAL.Entities;
using FinApp.Attributes;
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

            user.RoleId = 1;

            var newUser = await _userService.CreateUserAsync(user);

            if (newUser == null)
                return BadRequest(new { message = "User already exists" });

            return Ok();
        }

        [ServiceFilter(typeof(AuthorizeAttribute))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _userService.GetAllAsync();

            if (users == null)
                return NoContent();

            return Ok(users);
        }

        [ServiceFilter(typeof(AuthorizeAttribute))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userService.UpdateAsync(userDTO);

            if (user == null)
                return BadRequest(new { message = "User Id is incorrect" });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var userInDb = await _userService.GetAsync(id);

            if (userInDb == null)
                return NotFound();

            await _userService.DeleteAsync(userInDb);

            return Ok();
        }
    }
}
