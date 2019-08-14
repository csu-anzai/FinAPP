using BLL.DTOs;
using BLL.Models.Exceptions;
using BLL.Services.ImplementedServices;
using BLL.Services.IServices;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using Moq;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace BLL.Tests
{
    internal class PasswordConfirmationCodeServiceTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IUserRepository> _userRepoMock;
        private Mock<IPasswordConfirmationCodeRepository> _passwordConfirmCodeRepoMock;
        private Mock<IEmailSenderService> _emailSenderServiceMock;
        private PasswordConfirmationCodeService _passwordConfirmationCodeService;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepoMock = new Mock<IUserRepository>();
            _passwordConfirmCodeRepoMock = new Mock<IPasswordConfirmationCodeRepository>();
            _emailSenderServiceMock = new Mock<IEmailSenderService>();
            _passwordConfirmationCodeService = new PasswordConfirmationCodeService(_unitOfWorkMock.Object, _userRepoMock.Object,
                _passwordConfirmCodeRepoMock.Object, _emailSenderServiceMock.Object);
        }

        [Test]
        public async Task ConfirmCodeIsAssigned()
        {
            //Arrange
            _unitOfWorkMock.Setup(s => s.Complete());

            var user = new User();
            _userRepoMock.Setup(s => s.SingleOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(Task.FromResult(user));

            //Act
            var forgotPasswordDto = new ForgotPasswordDTO { Email = "email" };
            var userWithAssignedCode = await _passwordConfirmationCodeService.SendConfirmationCodeAsync(forgotPasswordDto);

            //Assert
            Assert.IsNotNull(userWithAssignedCode.PasswordConfirmationCode);
        }

        [Test]
        public void ConfirmCodeAssignmentThrowsUserNotFound()
        {
            //Arrange
            _unitOfWorkMock.Setup(s => s.Complete());

            _userRepoMock.Setup(s => s.SingleOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(Task.FromResult((User)null));

            //Act
            var forgotPasswordDto = new ForgotPasswordDTO { Email = "notValidEmail" };

            //Assert
            var exception = Assert.ThrowsAsync<ApiException>(async () =>
                await _passwordConfirmationCodeService.SendConfirmationCodeAsync(forgotPasswordDto));
            Assert.AreEqual(HttpStatusCode.NotFound, exception.Code);
        }

        [Test]
        public async Task ConfirmCodeValidation()
        {
            //Arrange
            const string confirmCode = "12345";
            var passwordConfirmationCode = new PasswordConfirmationCode
            {
                Code = confirmCode,
                CreateDate = DateTime.Now
            };
            _passwordConfirmCodeRepoMock.Setup(s => s.GetPasswordConfirmationCodeByUserIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(passwordConfirmationCode));

            //Act
            var passwordConfirmationCodeDto = new PasswordConfirmationCodeDTO
            {
                UserId = 1,
                Code = confirmCode
            };
            var isValidCode = await _passwordConfirmationCodeService.ValidateConfirmationCodeAsync(passwordConfirmationCodeDto);

            //Assert
            Assert.IsTrue(isValidCode);
        }

        [Test]
        public void ConfirmCodeValidationThrowsTimeoutExpired()
        {
            //Arrange
            const string confirmCode = "12345";

            TimeSpan passwordCodeTimeout = PasswordConfirmationCodeService.PasswordCodeTimeout;
            var createDate = DateTime.Now.Subtract(passwordCodeTimeout);
            var outdatedPasswordConfirmationCode = new PasswordConfirmationCode
            {
                Code = confirmCode,
                CreateDate = createDate
            };

            _passwordConfirmCodeRepoMock.Setup(s => s.GetPasswordConfirmationCodeByUserIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(outdatedPasswordConfirmationCode));

            //Act
            var passwordConfirmationCodeDto = new PasswordConfirmationCodeDTO
            {
                UserId = 1,
                Code = confirmCode
            };

            //Assert
            var exception = Assert.ThrowsAsync<ApiException>(async () =>
                await _passwordConfirmationCodeService.ValidateConfirmationCodeAsync(passwordConfirmationCodeDto));
            Assert.AreEqual(HttpStatusCode.Gone, exception.Code);
        }
    }
}
