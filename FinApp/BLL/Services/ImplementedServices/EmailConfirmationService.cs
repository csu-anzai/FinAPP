using BLL.DTOs;
using BLL.Models.Exceptions;
using BLL.Security.Jwt;
using BLL.Services.IServices;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IUserService _userService;
        private readonly JwtManager _jwtManager;

        private readonly string _message = " is the code to reset your password.\n" +
                                          $"The code is valid for PasswordCodeTimeout.Minutes minutes.";

        public EmailConfirmationService(IUnitOfWork unitOfWork, IEmailSenderService emailService, JwtManager jwtManager)
        {
            _unitOfWork = unitOfWork;
            _emailSenderService = emailService;
            _jwtManager = jwtManager;
        }

        public async Task SendConfirmEmailLinkAsync(ConfirmEmailDTO confirmEmailDto)
        {
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Id == confirmEmailDto.UserId);
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
            // validate token
            user.EmailConfirmed = true;
            await _unitOfWork.Complete();
        }


    }
}
