using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class ConfirmEmailDTO
    {
        public int UserId { get; set; }
        public string CallbackUrl { get; set; }
    }
}
