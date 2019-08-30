using BLL.DTOs;
using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IUploadService
    {
        Task<User> UploadUserAvatar(AvatarDTO avatarDTO);
    }
}
