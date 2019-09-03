using BLL.DTOs;
using BLL.Models.ViewModels;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IExpenseService
    {
        Task<ExpenseAddViewModel> AddExpenseAsync(ExpenseAddViewModel expense);
        Task<Account> Remove(int id);
        Task<ExpenseDTO> UpdateExpense(ExpenseUpdateViewModel expense);
        Task<IEnumerable<ExpenseDTO>> GetExpensesWithDetailsAndConditionAsync(TransactionOptions options);
    }
}
