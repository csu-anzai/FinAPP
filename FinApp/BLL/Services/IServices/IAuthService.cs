using DAL.DTOs;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IAuthService
    {
        Task<TokenDTO> SignInAsync(UserLoginDTO user);
    }
}
