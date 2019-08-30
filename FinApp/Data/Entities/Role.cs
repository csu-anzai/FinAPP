using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DAL.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new Collection<User>();
        }
    }
}
