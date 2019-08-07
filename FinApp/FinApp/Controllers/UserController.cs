using AutoMapper;
using BLL.Services.IServices;
using DAL.Context;
using DAL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        //-- Updated! --//

        private FinAppContext _context;

        // Take information from db context and return it to client in a list view
        [HttpGet("getUsers")]
        public IList<User> GetUsers()
        {
            var query = _context.Users.ToList();
            return query;
        }

        [HttpPost("deleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody]User user)
        {
            try
            {
               // _context.Users.Remove(user);
               // await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Uses finapp context class and dependency injection to get 
        // an instance of context and put this context to local variable
        public UserController(IUserService userService, IMapper mapper, FinAppContext context)
        {
            _userService = userService;
            _mapper = mapper;
            _context = context;
        }

        //-- Old! --//

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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var user = await _userService.GetAsync(id);

            if (user == null)
                return NotFound();

            var userDTO = _mapper.Map<User, UserDTO>(user);
         
            return Ok(userDTO);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id, UserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userService.UpdateAsync(userDTO);

            if (user == null)
                return BadRequest(new { message = "User Id is incorrect" });

            return Ok();
        }
    }
}
