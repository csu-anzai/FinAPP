using BLL.DTOs;
using BLL.Models.ViewModels;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IAuthService
    {
        Task<TokenDTO> GoogleSignInAsync(string email);
        Task<TokenDTO> SignInAsync(LoginViewModel user);
    }
}
