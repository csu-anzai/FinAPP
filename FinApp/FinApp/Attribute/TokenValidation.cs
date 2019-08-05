using BLL.Security.Jwt;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FinApp.Middlewares;
using Microsoft.AspNetCore.Mvc.Filters;
using BLL.Models.Exceptions;

namespace FinApp.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class TokenValidation : System.Attribute
    {
        private JwtManager _jwtManager;
        private Token _refreshToken;
        private IUserRepository _userRepository; 
        public TokenValidation(JwtManager jwtManager, Token refreshToken, IUserRepository userRepository)

        {
            _jwtManager = jwtManager;
            _refreshToken = refreshToken;
            _userRepository = userRepository;
        }

        public async Task CheckToken(AuthorizationFilterContext context)
        {
            var accessToken = context.HttpContext.Request.Headers["Authorization"].ToString();


            if (String.IsNullOrEmpty(accessToken))
                throw new HttpStatusCodeException(HttpStatusCode.Unauthorized);

            if (_jwtManager.IsExpired(accessToken) && !_jwtManager.IsExpired(_refreshToken.RefreshToken))
            {
                User user = await _userRepository.GetAsync(int.Parse(_jwtManager.GetPrincipalFromExpiredToken(accessToken).jwt.Subject));
                var newAccessToken = _jwtManager.GenerateAccessToken(user.Id, user.Email, user.Role.Name);

                context.HttpContext.Response.Headers.Add("newAccess_token", newAccessToken);
            }

            else if (_jwtManager.IsExpired(accessToken) && _jwtManager.IsExpired(_refreshToken.RefreshToken))
            {
                User user = await _userRepository.GetAsync(int.Parse(_jwtManager.GetPrincipalFromExpiredToken(_refreshToken.RefreshToken).jwt.Subject));

                var newAccessToken = _jwtManager.GenerateAccessToken(user.Id, user.Email, user.Role.Name);
                var newRefreshToken = _jwtManager.GenerateRefreshToken(user.Id, user.Email, user.Role.Name);

                context.HttpContext.Response.Headers.Add("newAccess_token", newAccessToken);
                await _jwtManager.UpdateAsync(user, newRefreshToken);
                

            }
        }

    }
}
