using DAL.Context;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories.ImplementedRepositories
{
    public class ExpenseRepository : BaseRepository<ExpenseCategory>, IExpenseCategoryRepository
    {
        public ExpenseRepository(FinAppContext context) : base(context)
        {
        }
    }
}
