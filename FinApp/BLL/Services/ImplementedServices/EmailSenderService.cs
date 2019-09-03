using BLL.DTOs;
using BLL.Models.ViewModels;
using BLL.Services.IServices;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailOptionsDTO _emailOptions;

        public EmailSenderService(IOptions<EmailOptionsDTO> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailOptions.SenderName, _emailOptions.Sender));
            mimeMessage.To.Add(new MailboxAddress(email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                await client.ConnectAsync(_emailOptions.MailServer, _emailOptions.MailPort, true);
                await client.AuthenticateAsync(_emailOptions.Sender, _emailOptions.Password);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
        public async Task SendContactEmailAsync(EmailViewModel emailVm)
        {
            var subject = $"From {emailVm.FullName}";
            var message = $"{emailVm.Message}\nUser's email:{emailVm.Email}";

            await SendEmailAsync(_emailOptions.ContactEmail, subject, message);
        }
    }
}
