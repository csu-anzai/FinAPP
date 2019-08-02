using BLL.Security.Jwt;
using BLL.Services.IServices;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtManager _jwtManager;

        public TokenService(IUnitOfWork unitOfWork, ITokenRepository tokenRepository, IRoleRepository roleRepository, JwtManager jwtManager)
        {
            _unitOfWork = unitOfWork;
            _tokenRepository = tokenRepository;
            _roleRepository = roleRepository;
            _jwtManager = jwtManager;
        }

        public async Task<Token> UpdateAsync(User user, string refreshToken)
        {
            var token = await _tokenRepository.GetTokenByUserId(user.Id);

            //updating
            token.RefreshToken = refreshToken;

            await _unitOfWork.Complete();

            return token;
        }
    }
}
