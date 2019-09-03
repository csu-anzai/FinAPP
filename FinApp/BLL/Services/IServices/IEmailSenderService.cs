using BLL.Models.ViewModels;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendContactEmailAsync(EmailViewModel emailVm);
    }
}
