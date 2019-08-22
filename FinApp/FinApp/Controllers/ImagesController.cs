using BLL.DTOs;
using BLL.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImagesController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ImageDTO> images = await imageService.GetAllAsync();

            return images.Any() ? Ok(images) : (IActionResult)NotFound();
        }

        [HttpPost]
        public ActionResult UploadAvatar(int id, IFormFile file)
        {
            if (file.Length > 0)
            {
                byte[] userAvatar = null;

                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    userAvatar = binaryReader.ReadBytes((int)file.Length);
                }

            }
            return Ok();
        }

    }
}