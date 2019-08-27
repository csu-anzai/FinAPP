using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.ViewModels
{
    public class NewPasswordViewModel
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
