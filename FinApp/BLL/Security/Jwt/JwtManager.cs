using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAL.DTOs;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;

namespace BLL.Security.Jwt
{
    public class JwtManager
    {
        private readonly JwtOptions _jwtOptions;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public JwtManager(IOptions<JwtOptions> jwtOptions, ITokenRepository tokenRepository, IUnitOfWork unitOfWork)
        {
            _jwtOptions = jwtOptions.Value;
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
  //          ThrowIfInvalidOptions(_jwtOptions);
        }
        public bool IsExpired(string accesToken)
        {
            var validationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _jwtOptions.SigningCredentials.Key,
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;

            try
            {
                tokenHandler.ValidateToken(accesToken, validationParameters, out validatedToken);
            }

            catch (Exception e)
            {
                var exeption = e.Message;
                return true;
            }
            validatedToken.ToString();
            return false;
        }
        public string[] GetClaims(string token)
        {

            var claims = new JwtSecurityToken(token).Subject.Split();

            return claims;
        }

        public (ClaimsPrincipal principal, JwtSecurityToken jwt) GetPrincipalFromExpiredToken(string token)
        {
            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = _jwtOptions.SigningCredentials.Key,
                        ValidateLifetime = false
                    },
                    out var securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null
                || !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");


            return (principal, jwtSecurityToken);
        }

        public string GenerateAccessToken(int userId, string login, string role) =>
            new JwtSecurityTokenHandler()
                .WriteToken(new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    notBefore: DateTime.UtcNow,
                    claims: GenerateClaims(userId, login, role),
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtOptions.AccessExpirationMins)),
                    signingCredentials: _jwtOptions.SigningCredentials
                ));

        public string GenerateRefreshToken(int userId, string login, string role) =>
            new JwtSecurityTokenHandler()
                .WriteToken(new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    notBefore: DateTime.UtcNow,
                    claims: GenerateClaims(userId, login, role),
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtOptions.RefreshExpirationMins)),
                    signingCredentials: _jwtOptions.SigningCredentials
            ));

        public TokenDTO GenerateToken(int userId, string login, string role) =>
            new TokenDTO
            {
                AccessToken = GenerateAccessToken(userId, login, role),
                RefreshToken = GenerateRefreshToken(userId, login, role)
            };

        private Claim[] GenerateClaims(int userId, string login, string role) =>
            new[]
            {
                new Claim(nameof(login), login),
                new Claim(nameof(role), role),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, _jwtOptions.JtiGenerator),
                new Claim(JwtRegisteredClaimNames.Iat,
                    ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64)
            };

        private long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round(
                (date.ToUniversalTime() - new DateTimeOffset(
                     1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);

        public async Task<Token> UpdateAsync(int userId, string refreshToken)
        {
            var token = await _tokenRepository.GetTokenByUserId(userId);

            token.RefreshToken = refreshToken;
            await _unitOfWork.Complete();

            return token;
        }

        private static void ThrowIfInvalidOptions(JwtOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtOptions.ValidFor));
            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(JwtOptions.SigningCredentials));
            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(JwtOptions.JtiGenerator));
        }
    }
}
