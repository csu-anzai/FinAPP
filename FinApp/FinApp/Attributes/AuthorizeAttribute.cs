using BLL.Models.Exceptions;
using BLL.Security.Jwt;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FinApp.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class AuthorizeAttribute : System.Attribute, IAsyncAuthorizationFilter
    {
        private JwtManager _jwtManager;
        private Token _refreshToken = new Token();
        private readonly ITokenRepository _tokenRepository;
        public AuthorizeAttribute(JwtManager jwtManager, ITokenRepository tokenRepository)

        {
            _jwtManager = jwtManager;
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

            if (String.IsNullOrEmpty(accessToken))
                throw new ApiException(HttpStatusCode.Unauthorized);

            if (_jwtManager.IsExpired(accessToken))
            {
                if (_jwtManager.IsExpired(_refreshToken.RefreshToken))
                {
                    var newRefreshToken = _jwtManager.GenerateRefreshToken(userId, userEmail, userRole);
                    await _jwtManager.UpdateAsync(userId, newRefreshToken);
                }
                throw new ApiException(HttpStatusCode.Unauthorized);
            }         
        }
    }

}

