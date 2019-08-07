﻿using AutoMapper;
using BLL.Models.Exceptions;
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
        public async Task<IActionResult> CreateUser(UserRegistrationDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            user.RoleId = 1;

            var newUser = await _userService.CreateUserAsync(user);

            if (newUser == null)
                return BadRequest(new ApiException(System.Net.HttpStatusCode.BadRequest, "User already exists"));

            return Ok();
        }

        [ServiceFilter(typeof(TokenValidation))]
        [HttpGet("accounts/{userId}")]
        public async Task<IActionResult> GetAccounts(int userId)
        {
            return Ok(await _userService.GetUserWithAccounts(userId));
        }
    }
}
