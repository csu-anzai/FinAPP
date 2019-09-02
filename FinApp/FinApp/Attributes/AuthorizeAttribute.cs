using BLL.Models.Exceptions;
using BLL.Security.Jwt;
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
        private readonly ITokenRepository _tokenRepository;
        public AuthorizeAttribute(JwtManager jwtManager, ITokenRepository tokenRepository)
        {
            _jwtManager = jwtManager;
            _tokenRepository = tokenRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var accessToken = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            if (String.IsNullOrEmpty(accessToken))
                throw new ApiException(HttpStatusCode.Unauthorized);

            var claims = _jwtManager.GetClaims(accessToken);

            var _refreshToken = await _tokenRepository.GetTokenByUserId(claims.Id);

            if (!_jwtManager.IsValid(accessToken))
            {
                throw new SecurityTokenException();
            }

            if (_jwtManager.IsExpired(accessToken))
            {

                if (_jwtManager.IsExpired(_refreshToken.RefreshToken))
                {

                    var newRefreshToken = _jwtManager.GenerateRefreshToken(claims.Id, claims.Email, claims.Role);
                    await _jwtManager.UpdateAsync(claims.Id, newRefreshToken);

                    // throw new ValidationException(HttpStatusCode.Unauthorized, nameof(HttpStatusCode.Unauthorized)); unlogin when refresh is expired

                }

                throw new ValidationException(HttpStatusCode.Unauthorized, nameof(HttpStatusCode.Unauthorized));

            }


        }
    }
}
