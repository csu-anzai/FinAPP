using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public IAuthRepository AuthRepository { get; }
        public UnitOfWork(DbContext context, IAuthRepository authRepository)
        {
            _context = context;

            AuthRepository = authRepository;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
