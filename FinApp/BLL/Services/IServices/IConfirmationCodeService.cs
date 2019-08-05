using DAL.DTOs;
using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IConfirmationCodeService
    {
        Task<User> SendConfirmationCode(ForgotPasswordDTO forgotPasswordDTO);
        Task<bool> ValidateConfirmationCode(ConfirmationCodeDTO confirmationCodeDTO);
    }
}
