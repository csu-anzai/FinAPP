using DAL.DTOs;
using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IAuthService : IService<User>
    {
        Task<User> SignInAsync(UserLoginDTO user);
        Task<User> SignUpAsync(User user);
    }
}
