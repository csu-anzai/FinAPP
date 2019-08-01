using AutoMapper;
using BLL.Security;
using BLL.Services.IServices;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class UserService : IUserService
    {
        protected IPassHasher _hasher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;

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
            user.Password = _hasher.HashPassword(user.Password);

            var token = new Token();
            token.User = user;

            await _tokenRepository.AddAsync(token);

            user.Token = token;
            user.TokenId = token.Id;
            await _userRepository.AddAsync(user);

            await _unitOfWork.Complete();

            return user;
        }

        public async Task<User> UpdateAsync(UserDTO user)
        {
            var upToDateUser = await _userRepository.SingleOrDefaultAsync(u => u.Id == user.Id);

            if (user == null)
                return null;

            _mapper.Map<UserDTO, User>(user, upToDateUser);
            await _unitOfWork.Complete();

            return upToDateUser;
        }

        public async Task<bool> IsExist(string email)
        {
            var existedUser = await _userRepository.SingleOrDefaultAsync(u => u.Email == email);

            if (existedUser == null)
                return false;

            return true;
        }
    }
}
