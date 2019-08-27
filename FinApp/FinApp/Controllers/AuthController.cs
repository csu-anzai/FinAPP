using AutoMapper;
using BLL.Models.ViewModels;
using BLL.Services.IServices;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IStringLocalizer<AuthController> _localizer;

        public AuthController(IAuthService userService, IMapper mapper, IStringLocalizer<AuthController> localizer)
        {
            _authService = userService;
            _mapper = mapper;
            _localizer = localizer;
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            var token = await _authService.SignInAsync(loginModel);

            if (token == null)
                return BadRequest(new { message = _localizer["InvalidCredentials"] });

            return Ok(new { token = token.AccessToken });
        }

        [HttpPost]
        [Route("signingoogle")]
        public async Task<IActionResult> GoogleSignIn(TokenViewModel googleToken)
        {
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(googleToken.IdToken);

            if (validPayload == null)
                return Ok(new { code = 401, message = _localizer["NonAuthorized"].Value });

            var token = await _authService.GoogleSignInAsync(validPayload.Email);

            if (token != null)
                return Ok(new { token = token.AccessToken });

            var googleProfile = _mapper.Map<RegistrationViewModel>(validPayload);
            return Ok(new { code = 404, googleProfile });
        }
    }
}
