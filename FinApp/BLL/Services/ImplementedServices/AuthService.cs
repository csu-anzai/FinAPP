﻿using BLL.Security;
using BLL.Services.IServices;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class AuthService : Service<User>, IAuthService
    {
        protected IPassHasher _hasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthRepository _authRepository;  

        public AuthService(IUnitOfWork unitOfWork, IAuthRepository authRepository, IPassHasher hasher) : base(unitOfWork, authRepository)
        {
            _hasher = hasher;
            _unitOfWork = unitOfWork;
            _authRepository = authRepository;
        }

        public async Task<User> SignInAsync(UserLoginDTO user)
        {
            var existedUser = await _authRepository.SingleOrDefaultAsync(u => u.Email == user.Email);

            if (existedUser == null)
                return null;

            // Check if password hashes are the same.
            // Now its throw an exeption
            //if (!_hasher.CheckPassWithHash(existedUser.Password, user.Password))
            //    return null;

            return existedUser;
        }

        public async Task<User> SignUpAsync(User user)
        {
            user.Password = _hasher.HashPassword(user.Password);
            return await base.CreateAsync(user);
        }
    }
}