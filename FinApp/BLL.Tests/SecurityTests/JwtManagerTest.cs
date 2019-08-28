using BLL.Security.Jwt;
using BLL.Security.Jwt.Models;
using DAL.UnitOfWork;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using System.Text;

namespace Tests
{
    public class JwtManagerTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IOptions<JwtOptions>> _jwtOptionsMock;
        private JwtManager _jwtManager;

        [SetUp]
        public void Setup()
        {
            var key = "$6AEQcCUzEX~pp6/[Y.L?V:)t";

            JwtOptions jwtOptions = new JwtOptions()
            {
                Audience = "http://localhost:44397",
                Issuer = "http://localhost:44397",
                AccessExpirationMins = 30,
                RefreshExpirationMins = 60 * 24 * 7,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
            };
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _jwtOptionsMock = new Mock<IOptions<JwtOptions>>();
            _jwtOptionsMock.Setup(ap => ap.Value).Returns(jwtOptions);

            _jwtManager = new JwtManager(_jwtOptionsMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public void IsExpiredReturnFalse_IfTokenNotEpired()
        {
            var userClaim = new UserClaims()
            {
                Email = "Email@gmail.com",
                Role = "User",
                Id = 1
            };
            var newToken = _jwtManager.GenerateAccessToken(userClaim.Id, userClaim.Email, userClaim.Role);

            Assert.False(_jwtManager.IsExpired(newToken));
        }

        [Test]
        public void IsExpiredReturnTrue_IfTokenEpired()
        {

            var expiredToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
                "eyJsb2dpbiI6InJvbS5pdmFuZXRzQGdtYWlsLmNvbSIsInJvbGUiOiJVc2VyIiwic3ViIjoiMiIsImp0aSI6IjNiOTMxMDQ2LWMzZjctNDI4MS1iODI1LTY3NjdkZDk3ZDhhMCIsImlhdCI6MTU2Njk5NjgzOSwibmJmIjoxNTY2OTk2ODM5LCJleHAiOjE1NjY5OTY4OTksImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDQzOTciLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQ0Mzk3In0." +
                    "h_TR-YN457opefv71rq42e-W5f3wKhCUYtg07TlWC0U";

            Assert.True(_jwtManager.IsExpired(expiredToken));
        }
    }
}