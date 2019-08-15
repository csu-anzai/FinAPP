using BLL.Models.Exceptions;
using BLL.Services.IServices;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class PasswordConfirmationCodeService : IPasswordConfirmationCodeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordConfirmationCodeRepository _codeRepository;
        private readonly IEmailSenderService _emailSenderService;
        public static TimeSpan PasswordCodeTimeout { get; } = new TimeSpan(0, 15, 0);

        private readonly string _message = " is the code to reset your password.\n" +
                                          $"The code is valid for {PasswordCodeTimeout.Minutes} minutes.";

        public PasswordConfirmationCodeService(IUnitOfWork unitOfWork, IUserRepository userRepository, IPasswordConfirmationCodeRepository codeRepository, IEmailSenderService emailService)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _codeRepository = codeRepository;
            _emailSenderService = emailService;
        }

        private static int GeneratePasswordConfirmationCode()
        {
            var passwordConfirmCode = new Random().Next(10000, 100000);
            return passwordConfirmCode;
        }

        private async Task AssignCodeAsync(User user)
        {
            if (user == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, "User doesn't exist.");
            }

            var generatedCode = GeneratePasswordConfirmationCode();

            user.PasswordConfirmationCode.Code = generatedCode.ToString();
            user.PasswordConfirmationCode.CreateDate = DateTime.Now;

            await _unitOfWork.Complete();
        }

        public async Task<User> SendConfirmationCodeAsync(ForgotPasswordDTO forgotPasswordDto)
        {
            var user = await _userRepository.SingleOrDefaultWithConfirmCodeAsync(u => u.Email == forgotPasswordDto.Email);
            if (user == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, "User with such email was not found.");
            }

            await AssignCodeAsync(user);
            var passwordConfirmCode = user.PasswordConfirmationCode.Code;

            var message = passwordConfirmCode + _message;

            await _emailSenderService.SendEmailAsync(forgotPasswordDto.Email, "Fin App: password reset code", message);

            return user;
        }

        public async Task<bool> ValidateConfirmationCodeAsync(PasswordConfirmationCodeDTO confirmationCodeDto)
        {
            var passwordConfirmCode = await _codeRepository.GetPasswordConfirmationCodeByUserIdAsync(confirmationCodeDto.UserId);

            TimeSpan timeAfterCodeCreation = DateTime.Now - passwordConfirmCode.CreateDate;
            if (timeAfterCodeCreation > PasswordCodeTimeout)
            {
                throw new ApiException(HttpStatusCode.Gone, "Reset password code timeout expired.");
            }

            var isValidCode = confirmationCodeDto.Code == passwordConfirmCode.Code;

            return isValidCode;
        }
    }
}
