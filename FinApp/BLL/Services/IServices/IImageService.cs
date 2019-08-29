using BLL.DTOs;
using BLL.Models.ViewModels;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IImageService
    {
        Task<ImageDTO> GetAsync(int id);
        Task<IEnumerable<ImageDTO>> GetAllAsync();
        Task AddImage(ImageViewModel imageVm);
        Task<Image> UpdateAsync(ImageDTO imageDto);
    }
}
