using DAL.Context;
using DAL.Entities;
using DAL.Repositories.IRepositories;

namespace DAL.Repositories.ImplementedRepositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(FinAppContext context) : base(context)
        {
        }
    }
}
