using BLL.DTOs;
using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IPasswordConfirmationCodeService
    {
        Task<User> SendConfirmationCodeAsync(ForgotPasswordDTO forgotPasswordDto);
        Task<bool> ValidateConfirmationCodeAsync(PasswordConfirmationCodeDTO confirmationCodeDto);
    }
}
