using BLL.Services.IServices;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class ConfirmationCodeService : IConfirmationCodeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IConfirmationCodeRepository _codeRepository;
        private readonly IEmailSenderService _emailSenderService;
        private const int passwCodeTimeoutMinutes = 15;

        public ConfirmationCodeService(IUnitOfWork unitOfWork, IUserRepository userRepository, IConfirmationCodeRepository codeRepository, IEmailSenderService emailService)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _codeRepository = codeRepository;
            _emailSenderService = emailService;
        }

        private async Task<int> AssignCodeAsync(User user)
        {
            int code = new Random().Next(10000, 100000);
            if (user != null)
            {
                user.ConfirmationCode = new ConfirmationCode
                {
                    Code = code.ToString(),
                    CreateDate = DateTime.Now
                };
            }

            await _unitOfWork.Complete();

            return code;
        }

        public async Task<User> SendConfirmationCode(ForgotPasswordDTO forgotPasswordDTO)
        {
            var user = await _userRepository.SingleOrDefaultAsync(u => u.Email == forgotPasswordDTO.Email);
            if (user == null)
            {
                return null;
            }
            int code = await AssignCodeAsync(user);

            string message = $"Here is the code to reset your password: {code}.\nThe code is valid for {passwCodeTimeoutMinutes} minutes";

            await _emailSenderService.SendEmailAsync(forgotPasswordDTO.Email, "Fin App: password reset code", message);

            return user;
        }

        public async Task<bool> ValidateConfirmationCode(ConfirmationCodeDTO confirmationCodeDTO)
        {
            var code = await _codeRepository.GetCodeByUserId(confirmationCodeDTO.UserId);

            TimeSpan timeAfterCodeCreation = DateTime.Now - code.CreateDate;
            if (timeAfterCodeCreation.Minutes > passwCodeTimeoutMinutes)
            {
                throw new TimeoutException("Reset code timeout expired");
            }

            bool isValidCode = false;
            if (confirmationCodeDTO.Code == code.Code)
            {
                isValidCode = true;
            }

            return isValidCode;
        }

    }
}
