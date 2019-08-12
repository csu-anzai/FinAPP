using AutoMapper;
using BLL.Security;
using BLL.Services.IServices;
using DAL.Context;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BLL.Services.ImplementedServices
{
    public class IncomeCategoryService : IIncomeCategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;

        public IncomeCategoryService(IMapper mapper, IUnitOfWork unitOfWork, IIncomeCategoryRepository incomeCategoryRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _incomeCategoryRepository = incomeCategoryRepository;
        }

        public async Task DeleteIncomeCategoryAsync(IncomeCategory incomeCategory)
        {
            _incomeCategoryRepository.Remove(incomeCategory);

            await _unitOfWork.Complete();
        }

        public async Task<IEnumerable<IncomeCategoryDTO>> GetAllIncomeCategoryAsync()
        {
            var incomeCategories = await _incomeCategoryRepository.GetAllAsync();
            var incomeCategoriesDTO = incomeCategories.Select(_mapper.Map<IncomeCategory, IncomeCategoryDTO>);
            return incomeCategoriesDTO.Count() > 0 ? incomeCategoriesDTO : null;
        }

        public async Task<IncomeCategory> GetIncomeCategoryAsync (int id)
        {
            var incomeCategory = await _incomeCategoryRepository.SingleOrDefaultAsync(u => u.Id == id);
            return incomeCategory ?? null;
        }

        public async Task<IncomeCategoryDTO> UpdateIncomeCategoryAsync(IncomeCategoryDTO incomeCategoryDTO)
        {
            var upToDateIncomeCategory = await _incomeCategoryRepository.SingleOrDefaultAsync(u =>u.Id == incomeCategoryDTO.Id);
            if (incomeCategoryDTO == null)
                return null;
            _mapper.Map(incomeCategoryDTO, upToDateIncomeCategory);
            await _unitOfWork.Complete();
            return incomeCategoryDTO;
        }

        public async Task<IncomeCategory> CreateIncomeCategoryAsync(IncomeCategory incomeCategory)
        {
            var existedCategory = await _incomeCategoryRepository.SingleOrDefaultAsync(u => u.Name == incomeCategory.Name);
            if (existedCategory != null)
                return null;
            await _incomeCategoryRepository.AddAsync(incomeCategory);
            await _unitOfWork.Complete();
            return incomeCategory;
        }
    }
}
