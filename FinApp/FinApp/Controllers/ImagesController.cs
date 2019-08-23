using BLL.DTOs;
using BLL.Models.ViewModels;
using BLL.Services.IServices;
using Microsoft.AspNetCore.Hosting;
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
        private const string _defaultFolderForCustomImages = "DefaultImages";
        private const string _defaultFolderForUploadImages = "Uploads";
        private readonly IImageService _imageService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImagesController(IHostingEnvironment hostingEnvironment, IImageService imageService)
        {
            _hostingEnvironment = hostingEnvironment;
            _imageService = imageService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ImageDTO> images = await _imageService.GetAllAsync();

            return images.Any() ? Ok(images) : (IActionResult)NotFound();
        }

        /// <summary>
        ///  FromForm attribute for FormData request
        /// </summary>
        /// <param name="imageVm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ImageViewModel imageVm)
        {
            // in helper create class for file saving
            var file = imageVm.Image;
            var folderName = _defaultFolderForUploadImages;
            var webRootPath = _hostingEnvironment.WebRootPath;
            var newPath = Path.Combine(webRootPath, folderName);

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            if (file.Length > 0)
            {
                // var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(newPath, imageVm.Name);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                await _imageService.AddImage(imageVm);
            }
            return Ok();
        }
    }
}
