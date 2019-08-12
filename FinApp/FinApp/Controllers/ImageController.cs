using BLL.Services.IServices;
using System.Threading.Tasks;
using DAL.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace FinApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ImageDTO> images = await imageService.GetAllAsync();

            return images.Any() ? Ok(images) : (IActionResult)NotFound();
        }
    }
}