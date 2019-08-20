using BLL.DTOs;
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
        Task<User> UpdateAsync(ProfileDTO profileDTO);
        Task DeleteAsync(UserDTO user);
        Task RecoverPasswordAsync(RecoverPasswordDTO recoverPasswordDto);
    }
}
