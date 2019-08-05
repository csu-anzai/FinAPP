using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class ConfirmationCodeDTO
    {
        public int UserId { get; set; }
        public string Code { get; set; }
    }
}
