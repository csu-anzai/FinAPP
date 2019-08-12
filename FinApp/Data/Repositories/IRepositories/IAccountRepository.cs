using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IAccountRepository: IBaseRepository<Account>
    {
        Task<Account> GetAccountById(int id);
        Task<Account> FindAsyncAccountWithImgCurrency(Expression<Func<Account, bool>> expression);
    }
}
