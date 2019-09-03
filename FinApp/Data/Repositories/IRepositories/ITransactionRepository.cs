using DAL.Entities;
using DAL.IRepositories;

namespace DAL.Repositories.IRepositories
{
    public interface ITransactionRepository: IBaseRepository<Transaction>
    {
        void Add(Transaction tr);
    }
}
