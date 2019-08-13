using BLL.Security.Jwt;
using DAL.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FinApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly JwtManager _jwtManager;

        public TokenController(JwtManager jwtManager)
        {
            _jwtManager = jwtManager;
        }

        [HttpPost]
        public IActionResult GetNewAccessToken(TokenDTO token)
        {
            var claims = _jwtManager.GetClaims(token.AccessToken);

            int userId = Convert.ToInt32(claims[0]);
            string email = claims[1];
            string role = claims[2];

            return Ok(new { token = _jwtManager.GenerateAccessToken(userId, email, role) });
        }
    }
}