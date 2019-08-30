using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface ITokenRepository : IBaseRepository <Token>
    {
        Task<Token> GetTokenByUserId(int id);
    }
}
