using BLL.DTOs;
using BLL.Security.Jwt;
using Microsoft.AspNetCore.Mvc;
using System;
using BLL.Models.ViewModels;

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
        public IActionResult GetNewAccessToken(TokenViewModel tokenModel)
        {
            _jwtManager.IsExpired(tokenModel.IdToken);
            var claims = _jwtManager.GetClaims(tokenModel.IdToken);

            int userId = Convert.ToInt32(claims[2]);
            string email = claims[0];
            string role = claims[1];

            return Ok(new { token = _jwtManager.GenerateAccessToken(userId, email, role) });
        }
    }
}