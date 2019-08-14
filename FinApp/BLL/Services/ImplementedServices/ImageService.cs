using AutoMapper;
using BLL.DTOs;
using BLL.Services.IServices;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository imageRepository;

        private readonly IMapper mapper;

        public ImageService(IImageRepository imageRepository, IMapper mapper)
        {
            this.imageRepository = imageRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ImageDTO>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<Image>, IEnumerable<ImageDTO>>(await imageRepository.GetAllAsync());
        }
    }
}
