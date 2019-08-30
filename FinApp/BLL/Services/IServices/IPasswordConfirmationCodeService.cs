using BLL.DTOs;
using BLL.Models.ViewModels;
using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IPasswordConfirmationCodeService
    {
        Task<User> SendConfirmationCodeAsync(ForgotPasswordViewModel forgotPasswordModel);
        Task<bool> ValidateConfirmationCodeAsync(PasswordConfirmationCodeDTO confirmationCodeDto);
    }
}
