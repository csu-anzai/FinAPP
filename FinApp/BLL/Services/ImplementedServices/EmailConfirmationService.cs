using BLL.DTOs;
using BLL.Models.Exceptions;
using BLL.Security.Jwt;
using BLL.Services.IServices;
using DAL.UnitOfWork;
using System.Net;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSenderService _emailSenderService;
        private readonly JwtManager _jwtManager;

        private readonly string _message;

        public EmailConfirmationService(IUnitOfWork unitOfWork, IEmailSenderService emailService, JwtManager jwtManager)
        {
            _unitOfWork = unitOfWork;
            _emailSenderService = emailService;
            _jwtManager = jwtManager;
            _message = "The link to confirm your email.\n" +
                       $"The link is valid for {_jwtManager.JwtOptions.AccessExpirationMins} minutes.\n";
        }

        public async Task SendConfirmEmailLinkAsync(ConfirmEmailDTO confirmEmailDto)
        {
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Email == confirmEmailDto.UserEmail);
            if (user == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, "User was not found.");
            }

            var role = await _unitOfWork.RoleRepository.GetAsync(user.RoleId);
            var accessToken = _jwtManager.GenerateAccessToken(user.Id, user.Email, role.Name);

            var callbackUrl = confirmEmailDto.CallbackUrl + accessToken;
            var message = _message + callbackUrl;

            await _emailSenderService.SendEmailAsync(user.Email, "Fin App: confirm email", message); ;
        }

        public async Task ValidateEmailLinkAsync(ValidateConfirmEmailDTO confirmEmailDto)
        {
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Id == confirmEmailDto.UserId);
            if (user == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, "User was not found.");
            }

            if (user.IsEmailConfirmed == true)
            {
                throw new ApiException(HttpStatusCode.Forbidden, "Email is already confirmed.");
            }

            var token = confirmEmailDto.AccessToken;

            if (_jwtManager.IsValid(token) && !_jwtManager.IsExpired(token))
            {
                user.IsEmailConfirmed = true;
                await _unitOfWork.Complete();
            }
            else
            {
                throw new ApiException(HttpStatusCode.Forbidden, "Validation failed.");
            }
        }
    }
}
