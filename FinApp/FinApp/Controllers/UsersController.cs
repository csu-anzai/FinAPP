using BLL.DTOs;
using BLL.Helpers;
using BLL.Models.Exceptions;
using BLL.Models.ViewModels;
using BLL.Services.IServices;
using FinApp.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace FinApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private IUploadService _uploadService;
        private readonly IUserService _userService;
        private readonly IStringLocalizer<UsersController> _localizer;

        public UsersController(IUserService userService, IUploadService uploadService, IHostingEnvironment hostingEnvironment, IStringLocalizer<UsersController> localizer)
        {
            _userService = userService;
            _uploadService = uploadService;
            _hostingEnvironment = hostingEnvironment;
            _localizer = localizer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegistrationViewModel registrationModels)
        {
            var fullPath = DefaultUserImagePath();

            var image = registrationModels.Avatar;

            if (string.IsNullOrEmpty(image))
                registrationModels.Avatar = ImageConvertor.GetImageFromPath(fullPath);
            else
                registrationModels.Avatar = await Downloader.GetImageAsBase64Url(image);

            var newUser = await _userService.CreateUserAsync(registrationModels);

            if (newUser == null)
                throw new ValidationException(HttpStatusCode.Forbidden, _localizer["UserAlreadyExists"].Value);

            return Ok();
        }

        [ServiceFilter(typeof(AuthorizeAttribute))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
                return NotFound();

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


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, ProfileDTO profileDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userService.UpdateAsync(profileDTO);

            if (user == null)
                return BadRequest(new { message = _localizer["UserIdIncorrect"].Value });

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

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword(NewPasswordViewModel newPassword)
        {
            if (!ModelState.IsValid || newPassword.Password != newPassword.ConfirmPassword)
                return BadRequest();

            await _userService.ChangePasswordAsync(newPassword);

            return Ok();
        }

        private string DefaultUserImagePath()
        {
            var webRootPath = _hostingEnvironment.WebRootPath;
            var newPath = Path.Combine(webRootPath, "DefaultImages");
            string fullPath = Path.Combine(newPath, "profile-icon.png");

            return fullPath;
        }
    }
}
