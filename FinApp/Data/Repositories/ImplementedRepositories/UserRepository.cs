using Data.Entities;
using Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.ImplementedRepositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context): base(context)
        {

        }
    }
}
