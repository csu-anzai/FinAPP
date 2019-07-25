using Data.Entities;
using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.IRepositories
{
    public interface IUserRepository: IBaseRepository<User>
    {
    }
}
