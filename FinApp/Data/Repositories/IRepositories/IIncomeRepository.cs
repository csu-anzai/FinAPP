using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IIncomeRepository : IBaseRepository<Income>
    {
        Task<Income> GetOneWithTransactionAsync(Expression<Func<Income, bool>> expression);
        Task<IEnumerable<Income>> GetAllWithDetailsAsync(int accountId, DateTime startDate, DateTime endDate);
    }
}
