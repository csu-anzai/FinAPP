using BLL.DTOs;
using BLL.Services.IServices;
using DAL.Entities;
using DAL.UnitOfWork;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class UploadService : IUploadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UploadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> UploadUserAvatar(AvatarDTO avatarDTO)
        {
            var upToDateUser = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Id == avatarDTO.UserId);

            if (upToDateUser == null)
                return null;

            upToDateUser.Avatar = System.Text.ASCIIEncoding.ASCII.GetBytes(avatarDTO.Avatar);

            await _unitOfWork.Complete();

            return upToDateUser;
        }
    }
}
