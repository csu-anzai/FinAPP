using System;

namespace DAL.Entities
{
    public class PasswordConfirmationCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime CreateDate { get; set; }

        public User User { get; set; }

    }
}
