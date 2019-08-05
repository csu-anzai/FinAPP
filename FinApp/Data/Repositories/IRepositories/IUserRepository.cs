using DAL.Entities;
using DAL.IRepositories;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserWithCodeByUserId(int id);
    }
}
