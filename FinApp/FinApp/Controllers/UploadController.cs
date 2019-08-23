using BLL.DTOs;
using BLL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly IUploadService _uploadService;
        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        public async Task<IActionResult> UserAvatar([FromForm]AvatarDTO avatar)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _uploadService.UploadUserAvatar(avatar);

            if (user == null)
                return NotFound();

            return Ok();
        }
    }
}
