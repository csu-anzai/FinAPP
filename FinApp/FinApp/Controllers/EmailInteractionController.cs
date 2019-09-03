using BLL.DTOs;
using BLL.Models.ViewModels;
using BLL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailInteractionController : Controller
    {
        private readonly IEmailConfirmationService _emailConfirmationService;
        private readonly IEmailSenderService _emailService;
        public EmailInteractionController(IEmailConfirmationService emailConfirmationService, IEmailSenderService emailService)
        {
            _emailConfirmationService = emailConfirmationService;
            _emailService = emailService;
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

        [HttpPost("admin")]
        public async Task<IActionResult> SendMessagetoAdmin(EmailViewModel emailVm)
        {
            await _emailService.SendEmailAsync(emailVm.Email, emailVm.Subject, emailVm.Message);
            return Ok();
        }
    }
}
