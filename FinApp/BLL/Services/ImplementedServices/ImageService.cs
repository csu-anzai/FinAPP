using AutoMapper;
using BLL.DTOs;
using BLL.Services.IServices;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.UnitOfWork;

namespace BLL.Services.ImplementedServices
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImageDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<Image>, IEnumerable<ImageDTO>>(await _unitOfWork.ImageRepository.GetAllAsync());
        }
    }
}
