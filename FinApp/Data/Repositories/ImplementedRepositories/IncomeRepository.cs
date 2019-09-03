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
    public class IncomeRepository : BaseRepository<Income>, IIncomeRepository
    {
        public IncomeRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Income>> GetAllWithDetailsAsync(int accountId)
        {
            return await _entities
                 .Include(i => i.IncomeCategory).ThenInclude(ic => ic.Image)
                 .Include(i => i.Transaction)
                 .Where(i => i.AccountId == accountId)
                 .ToListAsync();
        }

        public async Task<Income> GetOneWithTransactionAsync(Expression<Func<Income, bool>> expression)
        {
            return await _entities
                .Include(t => t.Transaction)
                .FirstOrDefaultAsync(expression);
        }

    }
}
