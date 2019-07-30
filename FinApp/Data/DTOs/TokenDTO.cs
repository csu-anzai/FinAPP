using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class TokenDTO
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
