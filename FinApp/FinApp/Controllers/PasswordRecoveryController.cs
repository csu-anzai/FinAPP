﻿using BLL.DTOs;
using BLL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DAL.Entities;
using BLL.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordRecoveryController : Controller
    {
        private readonly IPasswordConfirmationCodeService _confirmPasswordService;

        public PasswordRecoveryController(IPasswordConfirmationCodeService service)
        {
            _confirmPasswordService = service;
        }

        [HttpPost("sendCode")]
        public async Task<IActionResult> SendCode(ForgotPasswordViewModel forgotPasswordDto)
        {
            var user = await _confirmPasswordService.SendConfirmationCodeAsync(forgotPasswordDto);
            return Ok(user);
        }

        [HttpPost("validateCode")]
        public async Task<IActionResult> ValidateCode(PasswordConfirmationCodeDTO confirmationCodeDto)
        {
            var isValid = await _confirmPasswordService.ValidateConfirmationCodeAsync(confirmationCodeDto);
            return Ok(isValid);
        }

    }
}
