using BLL.DTOs;
using BLL.Security.Jwt;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FinApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : Controller
    {
        private readonly JwtManager _jwtManager;

        public TokensController(JwtManager jwtManager)
        {
            _jwtManager = jwtManager;
        }

        [HttpPost]
        public IActionResult GetNewAccessToken(TokenDTO token)
        {
            _jwtManager.IsExpired(token.AccessToken);
            var claims = _jwtManager.GetClaims(token.AccessToken);

            int userId = Convert.ToInt32(claims[2]);
            string email = claims[0];
            string role = claims[1];

            return Ok(new { token = _jwtManager.GenerateAccessToken(userId, email, role) });
        }
    }
}