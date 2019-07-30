using BLL.Security;
using BLL.Services.IServices;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class UserService : Service<User>, IUserService
    {
        protected IPassHasher _hasher;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IPassHasher hasher) : base(unitOfWork, userRepository)
        {
            _hasher = hasher;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.Password = _hasher.HashPassword(user.Password);
            return await CreateAsync(user);
        }
    }
}
