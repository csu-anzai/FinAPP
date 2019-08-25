using Microsoft.AspNetCore.Http;

namespace BLL.DTOs
{
    public class AvatarDTO
    {
        public string UserId { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
