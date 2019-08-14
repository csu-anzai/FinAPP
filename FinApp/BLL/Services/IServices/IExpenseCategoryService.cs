using BLL.DTOs;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IExpenseCategoryService
    {
        Task<ExpenseCategory> CreateExpenseCategoryAsync(ExpenseCategory expenseCategory);
        Task DeleteExpenseCategoryAsync(ExpenseCategory expenseCategory);
        Task<ExpenseCategory> UpdateExpenseCategoryAsync(ExpenseCategoryDTO expenseCategoryDTO);
        Task<ExpenseCategory> GetExpenseCategoryAsync(int id);
        Task<IEnumerable<ExpenseCategoryDTO>> GetAllExpenseCategoryAsync();

    }
}
