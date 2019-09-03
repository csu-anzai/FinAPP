using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IExpenseRepository : IBaseRepository<Expense>
    {
        Task<Expense> GetOneWithTransactionAsync(Expression<Func<Expense, bool>> expression);
        Task<IEnumerable<Expense>> GetAllWithDetailsAsync(int accountId, DateTime startDate, DateTime endDate);
    }
}
