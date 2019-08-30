using BLL.DTOs;
using BLL.Models.Exceptions;
using BLL.Models.ViewModels;
using BLL.Security;
using BLL.Security.Jwt;
using BLL.Services.IServices;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System.Net;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class AuthService : IAuthService
    {
        protected IPassHasher _hasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtManager _jwtManager;

        public AuthService(IUnitOfWork unitOfWork, IPassHasher hasher, IRoleRepository roleRepository, JwtManager jwtManager)
        {
            _unitOfWork = unitOfWork;
            _hasher = hasher;
            _jwtManager = jwtManager;
        }

        public async Task<TokenDTO> SignInAsync(LoginViewModel loginModel)
        {
            var existedUser = await _unitOfWork.AuthRepository.SingleOrDefaultAsync(u => u.Email == loginModel.Email);

            if (existedUser == null)
                return null;

            if (!_hasher.CheckPassWithHash(loginModel.Password, existedUser.Password))
                return null;

            if (existedUser.IsEmailConfirmed == false)
                throw new ApiException(HttpStatusCode.Forbidden, "Email must be confirmed before signing in.");

            var token = await GenerateNewTokensAsync(existedUser);

            var refreshToken = SetUpRefreshToken(existedUser, token.RefreshToken);

            await _jwtManager.UpdateAsync(existedUser.Id, refreshToken);

            return token;
        }

        public async Task<TokenDTO> GoogleSignInAsync(string email)
        {
            var existedUser = await _unitOfWork.AuthRepository.SingleOrDefaultAsync(u => u.Email == email);

            if (existedUser == null)
                return null;


            var token = await GenerateNewTokensAsync(existedUser);

            var refreshToken = SetUpRefreshToken(existedUser, token.RefreshToken);

            await _jwtManager.UpdateAsync(existedUser.Id, refreshToken);

            return token;
        }

        private async Task<TokenDTO> GenerateNewTokensAsync(User user)
        {
            var role = await _unitOfWork.RoleRepository.GetAsync(user.RoleId);
            var token = _jwtManager.GenerateToken(user.Id, user.Email, role?.Name);

            return token;
        }

        private string SetUpRefreshToken(User user, string token)
        {
            var refreshToken = new Token();
            refreshToken.RefreshToken = token;
            refreshToken.User = user;
            refreshToken.User.Id = user.Id;

            return refreshToken.RefreshToken;
        }
    }
}
