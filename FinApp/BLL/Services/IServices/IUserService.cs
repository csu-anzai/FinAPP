using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IUserService : IService<User>
    {
        Task<User> CreateUserAsync(User user);
        User GetUserWithAccounts(int id);
    }
}
