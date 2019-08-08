using BLL.Models.Exceptions;
using BLL.Security.Jwt;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FinApp.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class TokenValidation : System.Attribute, IAsyncAuthorizationFilter
    {
        private JwtManager _jwtManager;
        private Token _refreshToken = new Token();
        private IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        public TokenValidation(JwtManager jwtManager, IUserRepository userRepository, ITokenRepository tokenRepository)

        {
            _jwtManager = jwtManager;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var accessToken = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var claims = _jwtManager.GetClaims(accessToken);

            int userId = Convert.ToInt32(claims[0]);
            string userEmail = claims[1];
            string userRole = claims[2];

            _refreshToken = await _tokenRepository.GetTokenByUserId(userId);

            if (_jwtManager.IsExpired(accessToken) && !_jwtManager.IsExpired(_refreshToken.RefreshToken))
            {                
                var newAccessToken = _jwtManager.GenerateAccessToken(userId, userEmail, userRole);

                context.HttpContext.Response.Headers.Add("token", newAccessToken);
                context.HttpContext.Request.Headers["Authorization"] = "Bearer " + newAccessToken;
            }

            else if (_jwtManager.IsExpired(accessToken) && _jwtManager.IsExpired(_refreshToken.RefreshToken))
            {
                var newAccessToken = _jwtManager.GenerateAccessToken(userId, userEmail, userRole);
                var newRefreshToken = _jwtManager.GenerateRefreshToken(userId, userEmail, userRole);

                context.HttpContext.Response.Headers.Add("token", newAccessToken);
                context.HttpContext.Request.Headers["Authorization"] = "Bearer " + newAccessToken;

                await _jwtManager.UpdateAsync(userId, newRefreshToken);
            }
        }
    }
}
