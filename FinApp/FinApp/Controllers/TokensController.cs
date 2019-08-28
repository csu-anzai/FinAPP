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
            var claims = _jwtManager.GetClaims(tokenModel.IdToken);

            return Ok(new { token = _jwtManager.GenerateAccessToken(claims.Id, claims.Email, claims.Role) });
        }
    }
}