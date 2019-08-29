using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.ImplementedRepositories
{
    public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(DbContext context) : base(context)
        {
        }
    }
}
