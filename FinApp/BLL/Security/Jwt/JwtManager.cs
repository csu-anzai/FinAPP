﻿using BLL.DTOs;
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
        public JwtOptions JwtOptions { get; }
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public JwtManager(IOptions<JwtOptions> jwtOptions, ITokenRepository tokenRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            JwtOptions = jwtOptions.Value;
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
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
            try
            {
                var validationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = JwtOptions.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = JwtOptions.Issuer,
                    ValidateIssuerSigningKey = true,                    
                    IssuerSigningKey = JwtOptions.SigningCredentials.Key,
                    ValidateLifetime = false,
                };

                var Handler = new JwtSecurityTokenHandler()
                     .ValidateToken(
                         token,
                         validationParameters,
                         out var securityToken);
            }
            catch(Exception e)
            {
                var message = e.Message;
                return false;
            }
            return true;
        }

        public string GenerateAccessToken(int userId, string login, string role)
        {
            var token = new JwtSecurityTokenHandler()
                  .WriteToken(new JwtSecurityToken(
                      issuer: JwtOptions.Issuer,
                      audience: JwtOptions.Audience,
                      notBefore: DateTime.UtcNow,
                      claims: GenerateClaims(userId, login, role),
                      expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(JwtOptions.AccessExpirationMins)),
                      signingCredentials: JwtOptions.SigningCredentials
                  ));
            return token;
        }
        public string GenerateRefreshToken(int userId, string login, string role)
        {
            var token = new JwtSecurityTokenHandler()
                   .WriteToken(new JwtSecurityToken(
                       issuer: JwtOptions.Issuer,
                       audience: JwtOptions.Audience,
                       notBefore: DateTime.UtcNow,
                       claims: GenerateClaims(userId, login, role),
                       expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(JwtOptions.RefreshExpirationMins)),
                       signingCredentials: JwtOptions.SigningCredentials
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
                new Claim(JwtRegisteredClaimNames.Jti, JwtOptions.JtiGenerator),
                new Claim(JwtRegisteredClaimNames.Iat,
                    ToUnixEpochDate(JwtOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64)
            };

        private long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round(
                (date.ToUniversalTime() - new DateTimeOffset(
                     1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);

        public async Task<Token> UpdateAsync(int Id, string refreshToken)
        {
            var token = await _tokenRepository.GetTokenByUserId(Id);

            token.RefreshToken = refreshToken;
            await _unitOfWork.Complete();

            return token;
        }

        private static void ThrowIfInvalidOptions(JwtOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(Jwt.JwtOptions.ValidFor));
            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(Jwt.JwtOptions.SigningCredentials));
            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(Jwt.JwtOptions.JtiGenerator));
        }
        

    }
}