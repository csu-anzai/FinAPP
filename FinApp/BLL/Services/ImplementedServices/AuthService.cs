using BLL.Security;
using BLL.Security.Jwt;
using BLL.Services.IServices;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class AuthService : IAuthService
    {
        protected IPassHasher _hasher;
        private readonly ITokenRepository _tokenRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly JwtManager _jwtManager;

        private IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository, IPassHasher hasher, IRoleRepository roleRepository, ITokenRepository tokenRepository, JwtManager jwtManager)
        {
            _authRepository = authRepository;
            _hasher = hasher;
            _roleRepository = roleRepository;
            _tokenRepository = tokenRepository;
            _jwtManager = jwtManager;

        }

        public async Task<TokenDTO> SignInAsync(UserLoginDTO user)
        {
            var existedUser = await _authRepository.SingleOrDefaultAsync(u => u.Email == user.Email);

            if (existedUser == null)
                return null;

            if (!_hasher.CheckPassWithHash(user.Password, existedUser.Password))
                return null;

            var role = await _roleRepository.GetAsync(existedUser.RoleId);
            var token = _jwtManager.GenerateToken(existedUser.Id, user.Email, role?.Name);


            var refreshToken = new Token();
            refreshToken.RefreshToken = token.RefreshToken;
            refreshToken.User = existedUser;
            refreshToken.User.Id = existedUser.Id;

            await _jwtManager.UpdateAsync(existedUser, refreshToken.RefreshToken);
            //await _tokenRepository.AddAsync(refreshToken);

            return token;
        }
    }
}
