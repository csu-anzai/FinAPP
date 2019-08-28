using BLL.DTOs;
using BLL.Security.Jwt.Models;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Security.Jwt
{
    public class JwtManager
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IUnitOfWork _unitOfWork;

        public JwtManager(IOptions<JwtOptions> jwtOptions,  IUnitOfWork unitOfWork)
        {
            _jwtOptions = jwtOptions.Value;
            _unitOfWork = unitOfWork;
        }

        public bool IsExpired(string accesToken)
        {
            var token = new JwtSecurityToken(accesToken);

            return DateTime.UtcNow > token.ValidTo;
        }

        public UserClaims GetClaims(string token)
        {
            var principal = new JwtSecurityToken(token);
                       
            List<string> claims = new List<string>();

            foreach (Claim claim in principal.Claims)
            {
                claims.Add(claim.Value);
            }

            UserClaims userClaims = new UserClaims()
            {
                Email = claims[0],
                Role = claims[1],
                Id = Convert.ToInt32(claims[2])
            };

            return userClaims;
        }

        public bool IsValid(string token)
        {
            var validationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _jwtOptions.Audience,
                ValidateIssuer = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _jwtOptions.SigningCredentials.Key,
                ValidateLifetime = false,
            };
            try
            {        
                var Handler = new JwtSecurityTokenHandler()
                     .ValidateToken(
                         token,
                         validationParameters,
                         out var securityToken);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public string GenerateAccessToken(int userId, string login, string role)
        {
            var token = new JwtSecurityTokenHandler()
                  .WriteToken(new JwtSecurityToken(
                      issuer: _jwtOptions.Issuer,
                      audience: _jwtOptions.Audience,
                      notBefore: DateTime.UtcNow,
                      claims: GenerateClaims(userId, login, role),
                      expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtOptions.AccessExpirationMins)),
                      signingCredentials: _jwtOptions.SigningCredentials
                  ));
            return token;
        }

        public string GenerateRefreshToken(int userId, string login, string role)
        {
            var token = new JwtSecurityTokenHandler()
                   .WriteToken(new JwtSecurityToken(
                       issuer: _jwtOptions.Issuer,
                       audience: _jwtOptions.Audience,
                       notBefore: DateTime.UtcNow,
                       claims: GenerateClaims(userId, login, role),
                       expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtOptions.RefreshExpirationMins)),
                       signingCredentials: _jwtOptions.SigningCredentials
               ));
            return token;
        }

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

        public async Task<Token> UpdateAsync(int Id, string refreshToken)
        {
            var token = await _unitOfWork.TokenRepository.GetTokenByUserId(Id);

            token.RefreshToken = refreshToken;
            await _unitOfWork.Complete();

            return token;
        }
        
        

    }
}
