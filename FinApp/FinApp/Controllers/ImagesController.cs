using BLL.DTOs;
using BLL.Services.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FinApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService imageService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImagesController(IHostingEnvironment hostingEnvironment, IImageService imageService)
        {
            _hostingEnvironment = hostingEnvironment;
            this.imageService = imageService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ImageDTO> images = await imageService.GetAllAsync();

            return images.Any() ? Ok(images) : (IActionResult)NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(/*ImageViewModel imageVm*/)
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest();
            }
        }
    }
}