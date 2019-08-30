using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Security.Jwt.Models
{
    public class UserClaims
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public int Id { get; set; }
    }
}
