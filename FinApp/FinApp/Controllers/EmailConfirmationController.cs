using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Services.IServices;
using FinApp.Attributes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailConfirmationController : Controller
    {
        private readonly IEmailConfirmationService _emailConfirmationService;

        public EmailConfirmationController(IEmailConfirmationService emailConfirmationService)
        {
            _emailConfirmationService = emailConfirmationService;
        }

        [HttpPost("sendConfirmEmailLink")]
        public async Task<IActionResult> SendConfirmEmailLinkAsync(ConfirmEmailDTO confirmEmailDto)
        {
            await _emailConfirmationService.SendConfirmEmailLinkAsync(confirmEmailDto);
            return Ok();
        }

        [HttpPost("validateEmailLink")]
        public async Task<IActionResult> ValidateEmailLinkAsync(ValidateConfirmEmailDTO confirmEmailDto)
        {
            await _emailConfirmationService.ValidateEmailLinkAsync(confirmEmailDto);
            return Ok();
        }

    }
}
