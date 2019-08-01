using System;

namespace DAL.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? CreateDate { get; set; }

        public User User { get; set; }
    }
}
