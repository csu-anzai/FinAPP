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
    public class ConfirmationCodeRepository : BaseRepository<ConfirmationCode>, IConfirmationCodeRepository
    {
        public ConfirmationCodeRepository(FinAppContext context) : base(context)
        {

        }
    }
}
