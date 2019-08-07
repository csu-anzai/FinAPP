using DAL.Context;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.ImplementedRepositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(FinAppContext context) : base(context)
        {
        }
        public override async Task<User> GetAsync (int id)
        {
            return await _entities.Include("Role").SingleOrDefaultAsync(u=>u.Id==id);
        }
    }
}
