using System.Threading.Tasks;
using BLL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using DAL.DTOs;

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
        public async Task<IActionResult> SendCode(ForgotPasswordDTO forgotPasswordDto)
        {
            await _confirmPasswordService.SendConfirmationCodeAsync(forgotPasswordDto);
            return Ok();
        }

        [HttpPost("validateCode")]
        public async Task<IActionResult> ValidateCode(PasswordConfirmationCodeDTO confirmationCodeDto)
        {
            var isValid = await _confirmPasswordService.ValidateConfirmationCodeAsync(confirmationCodeDto);
            return Ok(isValid);
        }

    }
}
