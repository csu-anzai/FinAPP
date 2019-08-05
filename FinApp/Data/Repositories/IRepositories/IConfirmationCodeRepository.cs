using DAL.Entities;
using DAL.IRepositories;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IConfirmationCodeRepository : IBaseRepository<ConfirmationCode>
    {
        Task<ConfirmationCode> GetCodeByUserId(int id);
    }
}
