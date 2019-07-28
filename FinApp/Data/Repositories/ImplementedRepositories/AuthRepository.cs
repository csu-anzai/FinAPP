using DAL.Context;
using DAL.Entities;
using DAL.Repositories.IRepositories;


namespace DAL.Repositories.ImplementedRepositories
{
    public class AuthRepository : BaseRepository<User>, IAuthRepository
    {
        public AuthRepository(FinAppContext context) : base(context)
        {
        }
    }
}
