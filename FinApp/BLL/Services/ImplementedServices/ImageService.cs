using AutoMapper;
using BLL.DTOs;
using BLL.Models.Exceptions;
using BLL.Models.ViewModels;
using BLL.Services.IServices;
using DAL.Entities;
using DAL.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<ImageDTO> GetAsync(int id)
        {
            var image = await _unitOfWork.ImageRepository.GetAsync(id);
            return _mapper.Map<ImageDTO>(image);
        }

        public async Task<IEnumerable<ImageDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<Image>, IEnumerable<ImageDTO>>(await _unitOfWork.ImageRepository.GetAllAsync());
        }

        public async Task AddImage(ImageViewModel imageVm)
        {
            var existedImg = await _unitOfWork.ImageRepository.SingleOrDefaultAsync(img => img.Path == imageVm.Path && img.Name == imageVm.Name);

            if (existedImg != null)
                throw new ApiException(System.Net.HttpStatusCode.Conflict, "The image already exist");

            var image = _mapper.Map<Image>(imageVm);

            await _unitOfWork.ImageRepository.AddAsync(image);
            await _unitOfWork.Complete();
        }
    }
}
