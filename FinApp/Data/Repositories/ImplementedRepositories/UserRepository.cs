using DAL.Context;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories.ImplementedRepositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(FinAppContext context) : base(context)
        {
        }

        public Task<User> GetUserWithCodeByUserId(int id)
        {
            return _entities.Include(u => u.ConfirmationCode).SingleOrDefaultAsync(u => u.Id == id);
        }

    }
}
