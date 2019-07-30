using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IUserService : IService<User>
    {
        Task<User> CreateUserAsync(User user);
    }
}
