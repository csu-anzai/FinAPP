using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DbContext _context;

        public IUserRepository UserRepository { get; }
        public UnitOfWork(DbContext context, IUserRepository userRepository)
        {
            _context = context;

            UserRepository = userRepository;
        }    

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
