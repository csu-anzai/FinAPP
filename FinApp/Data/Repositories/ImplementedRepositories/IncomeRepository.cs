using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.ImplementedRepositories
{
    public class IncomeRepository : BaseRepository<Income>, IIncomeRepository
    {
        public IncomeRepository(DbContext context) : base(context)
        {
        }
    }
}
