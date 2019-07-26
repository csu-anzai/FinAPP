using BLL.Services.IServices;
using DAL.Entities;
using DAL.IRepositories;
using DAL.UnitOfWork;
using DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.Security;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class UserService: Service<User>, IUserService
    {
        protected IPassHasher _hasher;
        IUnitOfWork _unitOfWork;


        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IPassHasher hasher): base(unitOfWork, userRepository)
        {
            _hasher = hasher;
            _unitOfWork = unitOfWork;
            
        }
       public async Task<User> SignInAsync(User user)
        {
            user.Password = _hasher.HashPassword(user.Password);
            return await base.CreateAsync(user);
        }
        
    }
}
