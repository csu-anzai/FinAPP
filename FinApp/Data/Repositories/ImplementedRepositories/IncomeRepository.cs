using DAL.Context;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories.ImplementedRepositories
{
    public class IncomeRepository : BaseRepository<IncomeCategory>, IIncomeCategoryRepository
    {
        public IncomeRepository(FinAppContext context): base(context)
        {
        }
    }
}
