using DAL.Repositories.IRepositories;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAuthRepository AuthRepository { get; }
        Task<int> SaveAsync();
    }
}