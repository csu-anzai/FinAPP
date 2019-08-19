using AutoMapper;
using BLL.DTOs;
using BLL.Services.IServices;
using DAL.Repositories.IRepositories;
using FinApp.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        IUserRepository userRepository { get; set; }

        public UsersController(IUserService userService, IMapper mapper, IUserRepository userRepository)
        {
            _userService = userService;
            _mapper = mapper; this.userRepository = userRepository;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserRegistrationDTO userDto)
        {
            var newUser = await _userService.CreateUserAsync(userDto);

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
            //  var user = await userRepository.GetAsync(id);

            return Ok(user);
        }

        [ServiceFilter(typeof(AuthorizeAttribute))]
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

        [ServiceFilter(typeof(AuthorizeAttribute))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userInDb = await _userService.GetAsync(id);

            if (userInDb == null)
                return NotFound();

            await _userService.DeleteAsync(userInDb);

            return Ok();
        }

        [HttpPut("recoverPassword")]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordDTO recoverPasswordDto)
        {
            await _userService.RecoverPasswordAsync(recoverPasswordDto);
            return Ok();
        }

    }
}
