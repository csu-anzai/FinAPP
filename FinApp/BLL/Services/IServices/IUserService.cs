﻿using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);
    }
}
