using DAL.Context;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;

namespace DAL.Repositories.ImplementedRepositories
{
    public class AccountRepository: BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(DbContext context) : base(context)
        {

        }

        public async Task<Account> GetAccountById(int id)
        {
            return await _entities.Include(i => i.Image).Include(c => c.Currency).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account> FindAsyncAccountWithImgCurrency(Expression<Func<Account, bool>> expression)
        {
            return await _entities.Include(i => i.Image).Include(c => c.Currency).FirstOrDefaultAsync(expression);
        }
    }
}
