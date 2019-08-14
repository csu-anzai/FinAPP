using DAL.Context;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories.ImplementedRepositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(FinAppContext context) : base(context)
        {
        }

        public override Task<User> GetAsync(int id)
        {
            return  _entities
                 .Include(u => u.Accounts).ThenInclude(a => a.Image)
                 .Include(u => u.Accounts).ThenInclude(a => a.Currency)
                 .Include(u => u.Accounts).ThenInclude(a => a.Incomes).ThenInclude(i => i.Transaction)
                 .Include(u => u.Accounts).ThenInclude(a => a.Expenses).ThenInclude(i => i.Transaction)
                 .Include(u => u.Accounts).ThenInclude(a => a.Expenses).ThenInclude(e => e.ExpenseCategory)
                 .Include(u => u.Accounts).ThenInclude(a => a.Incomes).ThenInclude(i => i.IncomeCategory)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
