using DAL.Context;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.ImplementedRepositories
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(FinAppContext context) : base(context)
        {

        }

        public Task<Token> GetTokenByUserId(int id)
        {
            return _entities.Include(u => u.User).SingleOrDefaultAsync(u=>u.User.Id == id);
        }
    }
}
