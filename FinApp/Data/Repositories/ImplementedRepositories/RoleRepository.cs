using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repositories.IRepositories;

namespace DAL.Repositories.ImplementedRepositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository (FinAppContext context) : base(context)
        {

        }
    }
}
