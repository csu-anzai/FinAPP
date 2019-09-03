using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IIncomeRepository : IBaseRepository<Income>
    {
        Task<IEnumerable<Income>> GetAllWithDetailsAsync(int accountId, DateTime startDate, DateTime endDate);
    }
}
