using BLL.DTOs;
using BLL.Helpers;
using BLL.Models.Exceptions;
using BLL.Models.ViewModels;
using BLL.Services.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            var webRootPath = _hostingEnvironment.WebRootPath;

            var result = await DirectoryManager.SaveFileInFolder(webRootPath, _defaultFolderForUploadImages, imageVm);

            if (result == null)
                throw new ApiException(System.Net.HttpStatusCode.InternalServerError);

            await _imageService.AddImage(imageVm);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImage(int id, ImageDTO imageDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _imageService.UpdateAsync(imageDTO);

            if (user == null)
                return BadRequest(new { message = "Can't update image" });

            return Ok();
        }
    }
}
