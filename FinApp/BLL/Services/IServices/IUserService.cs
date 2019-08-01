using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IUserService
    {
        Task<bool> IsExist(string email);
        Task<User> CreateUserAsync(User user);
    }
}
