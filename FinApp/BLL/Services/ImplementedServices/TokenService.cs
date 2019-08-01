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
        private readonly ILogger _logger;

        public TokenService(IUnitOfWork unitOfWork, ITokenRepository tokenRepository, IRoleRepository roleRepository, JwtManager jwtManager, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _tokenRepository = tokenRepository;
            _roleRepository = roleRepository;
            _jwtManager = jwtManager;
            _logger = logger;
        }

        public async Task<Token> UpdateAsync(User user)
        {
            var token = await _tokenRepository.GetTokenByUserId(user.Id);

            //updating
            //token.RefreshToken = 
            //token.DateTime = DateTime.Now;

            await _unitOfWork.Complete();

            return token;
        }
    }
}
