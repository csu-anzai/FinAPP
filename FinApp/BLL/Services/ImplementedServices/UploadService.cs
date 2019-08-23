using BLL.DTOs;
using BLL.Helpers;
using BLL.Models.Exceptions;
using BLL.Services.IServices;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
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
            var userId = Int32.Parse(avatarDTO.UserId);

            var upToDateUser = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Id == userId);

            if (upToDateUser == null)
                throw new ApiException(System.Net.HttpStatusCode.NotFound, "User doesn't exist");

            upToDateUser.Avatar = ImageConvertor.GetByte64FromImage(avatarDTO.Avatar);

            await _unitOfWork.Complete();

            return upToDateUser;
        }
    }
}
