using DAL.Context;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.ImplementedRepositories
{
    public class PasswordConfirmationCodeRepository : BaseRepository<PasswordConfirmationCode>, IPasswordConfirmationCodeRepository
    {
        public PasswordConfirmationCodeRepository(FinAppContext context) : base(context)
        {

        }

        public Task<PasswordConfirmationCode> GetPasswordConfirmationCodeByUserId(int id)
        {
            return _entities.Include(u => u.User).SingleOrDefaultAsync(u => u.User.Id == id);
        }
    }
}
