using Microsoft.IdentityModel.Tokens;
using System;

namespace BLL.Security.Jwt
{
    public class JwtOptions
    {
        public const string Role = "role";
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public int AccessExpirationMins { get; set; } = 2;
        public int RefreshExpirationMins { get; set; }
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor => TimeSpan.FromMinutes(AccessExpirationMins);
        public string JtiGenerator => Guid.NewGuid().ToString();
        public SigningCredentials SigningCredentials { get; set; }
    }
}
