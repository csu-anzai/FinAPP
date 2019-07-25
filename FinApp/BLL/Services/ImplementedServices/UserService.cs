using BLL.Services.IServices;
using DAL.Entities;
using DAL.IRepositories;
using DAL.UnitOfWork;
using DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services.ImplementedServices
{
    public class UserService: Service<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository): base(unitOfWork, userRepository)
        {
        }
    }
}
