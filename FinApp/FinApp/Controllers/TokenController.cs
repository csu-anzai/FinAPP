using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Security.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("getToken")]
        public IActionResult GetNewAccessToken(string accessToken)
        {
            var claims = _jwtManager.GetClaims(accessToken);

            int userId = Convert.ToInt32(claims[0]);
            string email = claims[1];
            string role = claims[2];           

            return Ok(new { newAccessToken = _jwtManager.GenerateAccessToken(userId, email, role) } );
        }



    }
}