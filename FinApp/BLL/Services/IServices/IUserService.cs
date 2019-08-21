using BLL.DTOs;
using BLL.Models.ViewModels;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(RegistrationViewModel userDTO);
        Task<UserDTO> GetAsync(int id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<User> UpdateAsync(ProfileDTO profileDTO);
        Task DeleteAsync(UserDTO user);
        Task RecoverPasswordAsync(RecoverPasswordDTO recoverPasswordDto);
    }
}
