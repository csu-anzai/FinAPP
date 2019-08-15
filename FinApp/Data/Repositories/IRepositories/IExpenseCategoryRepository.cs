using DAL.Entities;
using DAL.IRepositories;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IExpenseCategoryRepository : IBaseRepository<ExpenseCategory>
    {
    }
}
