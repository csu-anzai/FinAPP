using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? CreateDate { get; set; }

        public int userId { get; set; }
        public User user { get; set; }



    }
}
