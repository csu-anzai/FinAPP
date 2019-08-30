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
using AutoMapper;
using DAL.Entities;


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
        private readonly IMapper _mapper;

        public string WebRootPath { get => _hostingEnvironment.WebRootPath; }

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _imageService.GetAsync(id);

            if (image == null)
                return NotFound();

            var imageDTO = _mapper.Map<Image, ImageDTO>(image);

            return Ok(imageDTO);
        }

        /// <summary>
        ///  FromForm attribute for FormData request
        /// </summary>
        /// <param name="imageVm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ImageViewModel imageVm)
        {
            var result = await DirectoryManager.SaveFileInFolder(WebRootPath, _defaultFolderForUploadImages, imageVm);

            if (result == null)
                throw new ApiException(System.Net.HttpStatusCode.InternalServerError);

            await _imageService.AddImage(imageVm);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var imageInDb = await _imageService.GetAsync(id);

            if (imageInDb == null)
                return NotFound();

            var root = $@"{WebRootPath}\{_defaultFolderForUploadImages}\{imageInDb.Path}\{imageInDb.Name}";
            DirectoryManager.RemoveFileFromFolder(root);

            await _imageService.DeleteImage(imageInDb);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImage(int id, ImageDTO imageDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var image = await _imageService.UpdateAsync(imageDTO);

            if (image == null)
                return BadRequest(new { message = "image id is incorrect!" });

            return Ok();
        }
    }
}
