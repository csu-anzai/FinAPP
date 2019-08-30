using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
    }
}
