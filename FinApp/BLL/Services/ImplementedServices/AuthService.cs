using BLL.Security;
using BLL.Security.Jwt;
using BLL.Services.IServices;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class AuthService : Service<User>, IAuthService
    {
        protected IPassHasher _hasher;
        private readonly ITokenRepository _tokenRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly JwtManager _jwtManager;

        public AuthService(IUnitOfWork unitOfWork, IAuthRepository authRepository, IPassHasher hasher, IRoleRepository roleRepository, ITokenRepository tokenRepository, JwtManager jwtManager) : base(unitOfWork, authRepository)
        {
            _hasher = hasher;
            _roleRepository = roleRepository;
            _tokenRepository = tokenRepository;
            _jwtManager = jwtManager;
        }

        public async Task<User> SignInAsync(UserLoginDTO user)
        {
            var existedUser = await _repository.SingleOrDefaultAsync(u => u.Email == user.Email);

            if (existedUser == null)
                return null;

            var role = await _roleRepository.GetAsync(existedUser.RoleId);
            var token = _jwtManager.GenerateToken(existedUser.Id, user.Email, role?.Name);
            Token refreshToken = new Token();

            refreshToken.RefreshToken = token.RefreshToken;
            refreshToken.UserId = existedUser.Id;

            //await _tokenRepository.AddAsync(refreshToken);

            if (!_hasher.CheckPassWithHash(user.Password, existedUser.Password))
                return null;

            return existedUser;
        }
    }
}
