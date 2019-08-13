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

        public async Task<User> GetAsync(int id)
        {
            return await FinAppContext.Users
                .Include(u => u.Accounts).ThenInclude(a => a.Image)
                .Include(u => u.Accounts).ThenInclude(a => a.Currency)
                .Include(u => u.Accounts).ThenInclude(a => a.Incomes).ThenInclude( i => i.Transaction)
                .Include(u => u.Accounts).ThenInclude(a => a.Expenses).ThenInclude(i => i.Transaction)
               .FirstOrDefaultAsync(u => u.Id == id);
        }


        protected FinAppContext FinAppContext { get { return _context as FinAppContext; } }
    }
}
