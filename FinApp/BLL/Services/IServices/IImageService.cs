using DAL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IImageService
    {
        Task<IEnumerable<ImageDTO>> GetAllAsync();
    }
}
