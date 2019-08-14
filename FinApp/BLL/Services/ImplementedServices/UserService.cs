using AutoMapper;
using BLL.Security;
using BLL.Services.IServices;
using DAL.Context;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BLL.Services.ImplementedServices
{
    public class UserService : IUserService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IPassHasher _hasher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, ITokenRepository tokenRepository, IPassHasher hasher, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _hasher = hasher;
            _mapper = mapper;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var existedUser = await _userRepository.SingleOrDefaultAsync(u => u.Email == user.Email);
            if (existedUser != null)
            {
                //_logger.Fatal("Email already existed, do not create User");
                return null;
            }
            user.Password = _hasher.HashPassword(user.Password);

            var token = new Token {User = user};
            user.Token = token;
            user.TokenId = token.Id;

            var confirmCode = new PasswordConfirmationCode {User = user};
            user.PasswordConfirmationCode = confirmCode;
            user.PasswordConfirmationCodeId = confirmCode.Id;

            await _tokenRepository.AddAsync(token);
            await _userRepository.AddAsync(user);

            await _unitOfWork.Complete();

            return user;
        }

        public async Task<User> UpdateAsync(UserDTO user)
        {
            var upToDateUser = await _userRepository.SingleOrDefaultAsync(u => u.Id == user.Id);

            if (user == null)
                return null;

            _mapper.Map(user, upToDateUser);

            await _unitOfWork.Complete();

            return upToDateUser;
        }

        public async Task<UserDTO> GetAsync(int id)
        {
            var user = await _userRepository.GetAsync(id);

            var userDTO = _mapper.Map<User, UserDTO>(user);

            return userDTO;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var usersDTO = users.Select(_mapper.Map<User, UserDTO>);
            
            return usersDTO.Any() ? usersDTO : null;
        }

        public async Task DeleteAsync(UserDTO userDTO)
        {
            var user = await _userRepository.SingleOrDefaultAsync(u => u.Id == userDTO.Id);

            _userRepository.Remove(user);

            await _unitOfWork.Complete();
        }
    }
}
