using BLL.DTOs;
using BLL.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;


namespace BLL.Services.IServices
{
    public interface IImageService
    {
        Task<Image> GetAsync(int id);
        Task<IEnumerable<ImageDTO>> GetAllAsync();
        Task AddImage(ImageViewModel imageVm);
        Task DeleteImage(Image image);
        Task<Image> ChangeImage(ImageDTO imageDTO);
    }
}
