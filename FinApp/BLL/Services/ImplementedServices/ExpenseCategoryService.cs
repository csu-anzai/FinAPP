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
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseCategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteExpenseCategoryAsync(ExpenseCategory expenseCategory)
        {
            _unitOfWork.ExpenseCategoryRepository.Remove(expenseCategory);
            await _unitOfWork.Complete();
        }

        public async Task<IEnumerable<ExpenseCategoryDTO>> GetAllExpenseCategoryAsync()
        {
            var expenseCategories = await _unitOfWork.ExpenseCategoryRepository.GetAllAsync();
            var expenseCategoriesDTO = expenseCategories.Select(_mapper.Map<ExpenseCategory, ExpenseCategoryDTO>);

            return expenseCategoriesDTO.Count() > 0 ? expenseCategoriesDTO : null;
        }

        public async Task<ExpenseCategory> GetExpenseCategoryAsync(int id)
        {
            var expenseCategory = await _unitOfWork.ExpenseCategoryRepository.SingleOrDefaultAsync(u => u.Id == id);
            return expenseCategory ?? null;
        }

        public async Task<ExpenseCategory> UpdateExpenseCategoryAsync(ExpenseCategoryDTO expenseCategoryDTO)
        {
            var upToDateExpenseCategory = await _unitOfWork.ExpenseCategoryRepository.SingleOrDefaultAsync(u => u.Id == expenseCategoryDTO.Id);
            if (expenseCategoryDTO == null)
                return null;
            _mapper.Map(expenseCategoryDTO, upToDateExpenseCategory);
            await _unitOfWork.Complete();
            return upToDateExpenseCategory;
        }

        public async Task<ExpenseCategory> CreateExpenseCategoryAsync(ExpenseCategory expenseCategory)
        {
            var existedCategory = await _unitOfWork.ExpenseCategoryRepository.SingleOrDefaultAsync(u => u.Name == expenseCategory.Name);
            if (existedCategory != null)
                return null;
            expenseCategory.ImageId = 1;
            await _unitOfWork.ExpenseCategoryRepository.AddAsync(expenseCategory);
            await _unitOfWork.Complete();
            return expenseCategory;
        }
    }
}
