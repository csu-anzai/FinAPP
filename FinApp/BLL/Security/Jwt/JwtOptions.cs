using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Security.Jwt
{
    class JwtOptions
    {
        public const string Role = "role";
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public int AccessExpirationMins { get; set; }
        public int RefreshExpirationMins { get; set; }
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor => TimeSpan.FromMinutes(AccessExpirationMins);
        public string JtiGenerator => Guid.NewGuid().ToString();
        public SigningCredentials SigningCredentials { get; set; }
    }
}
