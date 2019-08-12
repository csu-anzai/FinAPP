using DAL.Context;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.ImplementedRepositories
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(DbContext context) : base(context)
        {
        }
    }
}
