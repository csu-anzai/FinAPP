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
            var accessToken = context.HttpContext.Request.Headers["Authorization"].ToString();


            if (String.IsNullOrEmpty(accessToken))
                throw new ApiException(HttpStatusCode.Unauthorized);

            User user = await _userRepository.GetAsync(int.Parse(_jwtManager.GetPrincipalFromExpiredToken(accessToken).jwt.Subject));
            _refreshToken = await _tokenRepository.GetTokenByUserId(user.Id);

            if (_jwtManager.IsExpired(accessToken) && !_jwtManager.IsExpired(_refreshToken.RefreshToken))
            {      
                
                var newAccessToken = _jwtManager.GenerateAccessToken(user.Id, user.Email, user.Role.Name);
                context.HttpContext.Response.Headers.Add("newAccess_token", newAccessToken);

            }

            else if (_jwtManager.IsExpired(accessToken) && _jwtManager.IsExpired(_refreshToken.RefreshToken))
            {

                var newAccessToken = _jwtManager.GenerateAccessToken(user.Id, user.Email, user.Role.Name);
                var newRefreshToken = _jwtManager.GenerateRefreshToken(user.Id, user.Email, user.Role.Name);

                context.HttpContext.Response.Headers.Add("newAccess_token", newAccessToken);
                await _jwtManager.UpdateAsync(user, newRefreshToken);


            }
        }
    }
}
