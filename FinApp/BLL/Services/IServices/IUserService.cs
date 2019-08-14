﻿using BLL.DTOs;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserRegistrationDTO userDTO);
        Task<UserDTO> GetAsync(int id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<User> UpdateAsync(UserDTO userDTO);
        Task DeleteAsync(UserDTO user);
    }
}
