using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories.ImplementedRepositories
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(DbContext context) : base(context)
        {

        }

        public Task<Token> GetTokenByUserId(int id)
        {
            // TODO: Rewrite
            return _entities.Include(u => u.User).SingleOrDefaultAsync(u => u.User.Id == id);
        }
    }
}
