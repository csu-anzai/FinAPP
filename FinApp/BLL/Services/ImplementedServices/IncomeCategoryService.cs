using AutoMapper;
using BLL.DTOs;
using BLL.Services.IServices;
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


        public IncomeCategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }

        public async Task DeleteIncomeCategoryAsync(IncomeCategory incomeCategory)
        {
            _unitOfWork.IncomeCategoryRepository.Remove(incomeCategory);

            await _unitOfWork.Complete();
        }

        public async Task<IEnumerable<IncomeCategoryDTO>> GetAllIncomeCategoryAsync()
        {
            var incomeCategories = await _unitOfWork.IncomeCategoryRepository.GetAllAsync();
            var incomeCategoriesDTO = incomeCategories.Select(_mapper.Map<IncomeCategory, IncomeCategoryDTO>);
            return incomeCategoriesDTO.Count() > 0 ? incomeCategoriesDTO : null;
        }

        public async Task<IncomeCategory> GetIncomeCategoryAsync(int id)
        {
            var incomeCategory = await _unitOfWork.IncomeCategoryRepository.SingleOrDefaultAsync(u => u.Id == id);
            return incomeCategory ?? null;
        }

        public async Task<IncomeCategory> UpdateIncomeCategoryAsync(IncomeCategoryDTO incomeCategoryDTO)
        {
            var upToDateIncomeCategory = await _unitOfWork.IncomeCategoryRepository.SingleOrDefaultAsync(u => u.Id == incomeCategoryDTO.Id);
            if (incomeCategoryDTO == null)
                return null;
            _mapper.Map(incomeCategoryDTO, upToDateIncomeCategory);
            await _unitOfWork.Complete();
            return upToDateIncomeCategory;
        }


        public async Task<IncomeCategory> CreateIncomeCategoryAsync(IncomeCategory incomeCategory)
        {
            var existedCategory = await _unitOfWork.IncomeCategoryRepository.SingleOrDefaultAsync(u => u.Name == incomeCategory.Name);
            if (existedCategory != null)
                return null;
            incomeCategory.ImageId = 1;
            await _unitOfWork.IncomeCategoryRepository.AddAsync(incomeCategory);
            await _unitOfWork.Complete();
            return incomeCategory;
        }
    }
}
