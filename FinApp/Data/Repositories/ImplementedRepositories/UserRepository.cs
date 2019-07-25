using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.ImplementedRepositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context): base(context)
        {

        }
        public User GetUserWithAccounts(int id)
        {
            return new User();
          
        }
    }
}
