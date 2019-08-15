using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class RecoverPasswordDTO
    {
        public int Id { get; set; }
        public string NewPassword { get; set; }
    }
}
