using DAL.DTOs;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IAuthService
    {
        Task<TokenDTO> GoogleSignInAsync(string email);
        Task<TokenDTO> SignInAsync(UserLoginDTO user);
    }
}
