using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Expense>> GetAllWithDetailsAsync(int accountId, DateTime startDate, DateTime endDate)
        {
            return await _entities
                 .Include(i => i.ExpenseCategory).ThenInclude(ic => ic.Image)
                 .Include(i => i.Transaction)
                 .Where(i => i.AccountId == accountId && i.Transaction.Date <= endDate && i.Transaction.Date >= startDate)
                 .OrderByDescending(i => i.Transaction.Date)
                 .ToListAsync();
        }

    }
}
