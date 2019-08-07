﻿using AutoMapper;
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

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserLoginDTO userDto)
        {
            var token = await _authService.SignInAsync(userDto);

            if (token == null)
                return BadRequest(new { message = "Credentials are invalid" });

            return Ok(new { token = token.AccessToken });
        }

        [HttpPost("google-signin")]
        public IActionResult GoogleSignIn(GoogleUserDTO userDto)
        {
            //var token = await _authService.SignInAsync(userDto);

            //if (token == null)
            //    return BadRequest(new { message = "Credentials are invalid" });

            return Ok(new { token = "goog" });
        }
    }
}
