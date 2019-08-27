using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class ValidateConfirmEmailDTO
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; }
    }
}
