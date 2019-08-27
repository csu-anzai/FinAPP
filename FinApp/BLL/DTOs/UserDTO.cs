using System;
using System.Collections.Generic;

namespace BLL.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Avatar { get; set; }
        public bool EmailConfirmed { get; set; }
        public int RoleId { get; set; }

        public ICollection<AccountDTO> Accounts { get; set; }
    }
}
