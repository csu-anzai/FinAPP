using AutoMapper;
using BLL.Services.IServices;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class CurrencyService: ICurrencyService
    {
        private readonly ICurrencyRepository currencyRepository;

        private readonly IMapper mapper;

        public CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper)
        {
            this.currencyRepository = currencyRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CurrencyDTO>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<Currency>,  IEnumerable<CurrencyDTO>>( await currencyRepository.GetAllAsync());
        }
    }
}
