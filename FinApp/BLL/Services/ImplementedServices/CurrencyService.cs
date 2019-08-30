using AutoMapper;
using BLL.DTOs;
using BLL.Services.IServices;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CurrencyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CurrencyDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<Currency>, IEnumerable<CurrencyDTO>>(await _unitOfWork.CurrencyRepository.GetAllAsync());
        }
    }
}
