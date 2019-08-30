using Microsoft.AspNetCore.Http;

namespace BLL.Models.ViewModels
{
    public class ImageViewModel
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public IFormFile Image { get; set; }
    }
}
