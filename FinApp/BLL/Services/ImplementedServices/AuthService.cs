using BLL.Security;
using BLL.Services.IServices;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class AuthService : Service<User>, IAuthService
    {
        protected IPassHasher _hasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthRepository _authRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthService(IUnitOfWork unitOfWork, IAuthRepository authRepository, IPassHasher hasher, IRoleRepository roleRepository) : base(unitOfWork, authRepository)
        {
            _hasher = hasher;
            _unitOfWork = unitOfWork;
            _authRepository = authRepository;
            _roleRepository = roleRepository;
        }

        public async Task<User> SignInAsync(UserLoginDTO user)
        {
            var existedUser = await _authRepository.SingleOrDefaultAsync(u => u.Email == user.Email);
            var role = await _roleRepository.GetAsync(existedUser.RoleId);
            if (existedUser == null)
                return null;

            
            if (!_hasher.CheckPassWithHash(user.Password, existedUser.Password))
                return null;

            return existedUser;
        }

        public async Task<User> SignUpAsync(User user)
        {
            user.Password = _hasher.HashPassword(user.Password);
            return await base.CreateAsync(user);
        }
    }
}
