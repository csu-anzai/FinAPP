using DAL.Context;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.ImplementedRepositories
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(FinAppContext context) : base(context)
        {

        }
    }
}
