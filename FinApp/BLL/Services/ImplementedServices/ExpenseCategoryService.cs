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
    public class ExpenseCategoryService: IExpenseCategoryService
    {
        private readonly IMapper _mapper;
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseCategoryService(IMapper mapper, IUnitOfWork unitOfWork, IExpenseCategoryRepository expenseCategoryRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _expenseCategoryRepository = expenseCategoryRepository;
        }

        public async Task DeleteExpenseCategoryAsync(ExpenseCategory expenseCategory)
        {
            _expenseCategoryRepository.Remove(expenseCategory);
            await _unitOfWork.Complete();
        }

        public async Task<IEnumerable<ExpenseCategoryDTO>> GetAllExpenseCategoryAsync()
        {
            var expenseCategories = await _expenseCategoryRepository.GetAllAsync();
            var expenseCategoriesDTO = expenseCategories.Select(_mapper.Map<ExpenseCategory, ExpenseCategoryDTO>);

            return expenseCategoriesDTO.Count() > 0 ? expenseCategoriesDTO : null;
        }
    }
}
