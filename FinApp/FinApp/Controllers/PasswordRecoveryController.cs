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
        private readonly IConfirmationCodeService _confirmService;

        public PasswordRecoveryController(IConfirmationCodeService service)
        {
            _confirmService = service;
        }

        [HttpPost("sendCode")]
        public async Task<IActionResult> SendCode(ForgotPasswordDTO forgotPasswordDTO)
        {
            await _confirmService.SendConfirmationCode(forgotPasswordDTO);
            return Ok();
        }

        [HttpPost("validateCode")]
        public async Task<IActionResult> ValidateCode(ConfirmationCodeDTO confirmationCodeDTO)
        {
            bool isValid=await _confirmService.ValidateConfirmationCode(confirmationCodeDTO);
            return Ok(isValid);
        }

    }
}
