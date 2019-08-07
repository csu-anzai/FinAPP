using DAL.DTOs;
using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IPasswordConfirmationCodeService
    {
        Task<User> SendConfirmationCode(ForgotPasswordDTO forgotPasswordDto);
        Task<bool> ValidateConfirmationCode(PasswordConfirmationCodeDTO confirmationCodeDto);
    }
}
