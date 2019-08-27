using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IEmailConfirmationService
    {
        Task SendConfirmEmailLinkAsync(ConfirmEmailDTO confirmEmailDto);
        Task ValidateEmailLinkAsync(ValidateConfirmEmailDTO confirmEmailDto);
    }
}
