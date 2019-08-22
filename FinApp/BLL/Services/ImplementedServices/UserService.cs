using AutoMapper;
using BLL.DTOs;
using BLL.Models.Exceptions;
using BLL.Models.ViewModels;
using BLL.Security;
using BLL.Services.IServices;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace BLL.Services.ImplementedServices
{
    public class UserService : IUserService
    {
        private readonly IPassHasher _hasher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IPassHasher hasher, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _hasher = hasher;
            _mapper = mapper;
        }

        public async Task<User> CreateUserAsync(RegistrationViewModel registrationModel)
        {
            var user = _mapper.Map<User>(registrationModel);

            user.RoleId = 1;

            var existedUser = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Email == user.Email);

            if (existedUser != null)
                return null;

            user.Password = _hasher.HashPassword(user.Password);

            var token = new Token { User = user };
            user.Token = token;

            var confirmCode = new PasswordConfirmationCode { User = user };
            user.PasswordConfirmationCode = confirmCode;

            await _unitOfWork.TokenRepository.AddAsync(token);
            await _unitOfWork.UserRepository.AddAsync(user);

            await _unitOfWork.Complete();

            return user;
        }

        public async Task<User> UpdateAsync(ProfileDTO profileDTO)
        {
            var upToDateUser = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Id == profileDTO.Id);

            if (upToDateUser == null)
                return null;

            _mapper.Map(profileDTO, upToDateUser);

            await _unitOfWork.Complete();

            return upToDateUser;
        }

        public async Task<UserDTO> GetAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(id);

            var userDTO = _mapper.Map<User, UserDTO>(user);

            return userDTO;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            var usersDTO = users.Select(_mapper.Map<User, UserDTO>);

            return usersDTO.Any() ? usersDTO : null;
        }

        public async Task DeleteAsync(UserDTO userDTO)
        {
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Id == userDTO.Id);

            _unitOfWork.UserRepository.Remove(user);

            await _unitOfWork.Complete();
        }


        public async Task ChangePasswordAsync(NewPasswordViewModel model)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(model.UserId);

            if (user == null) {
                throw new ApiException(HttpStatusCode.NotFound, "User was not found"); }

            if (!_hasher.CheckPassWithHash(model.OldPassword, user.Password))
                throw new ApiException(HttpStatusCode.BadRequest, "Old password incorrect");

            user.Password = _hasher.HashPassword(model.Password);

            await _unitOfWork.Complete();       
        }

        public async Task RecoverPasswordAsync(RecoverPasswordDTO recoverPasswordDto)
        {
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Id == recoverPasswordDto.Id);
            if (user == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, "User was not found.");
            }

            user.Password = _hasher.HashPassword(recoverPasswordDto.NewPassword);

            await _unitOfWork.Complete();
        }

        
    }
}
