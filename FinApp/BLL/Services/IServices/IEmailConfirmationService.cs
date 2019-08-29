using BLL.DTOs;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IEmailConfirmationService
    {
        Task SendConfirmEmailLinkAsync(ConfirmEmailDTO confirmEmailDto);
        Task ValidateEmailLinkAsync(ValidateConfirmEmailDTO confirmEmailDto);
    }
}
