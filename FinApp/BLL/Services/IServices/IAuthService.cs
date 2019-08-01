using DAL.DTOs;
using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IAuthService
    {
        Task<User> SignInAsync(UserLoginDTO user);
    }
}
