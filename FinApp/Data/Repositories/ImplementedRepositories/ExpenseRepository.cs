using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.ImplementedRepositories
{
    public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(DbContext context) : base(context)
        {

        }

        public async Task<Expense> GetOneWithTransactionAsync(Expression<Func<Expense, bool>> expression)
        {
            return await _entities
                .Include(t => t.Transaction)
                .FirstOrDefaultAsync(expression);
        }

    }
}
