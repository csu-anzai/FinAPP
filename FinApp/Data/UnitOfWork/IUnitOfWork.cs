using DAL.Repositories.IRepositories;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task Complete();
    }
}