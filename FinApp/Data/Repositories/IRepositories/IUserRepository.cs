using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> SingleOrDefaultWithConfirmCodeAsync(Expression<Func<User, bool>> expression);
    }
}
