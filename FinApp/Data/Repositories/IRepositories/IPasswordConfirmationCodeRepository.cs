using DAL.Entities;
using DAL.IRepositories;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IPasswordConfirmationCodeRepository : IBaseRepository<PasswordConfirmationCode>
    {
        Task<PasswordConfirmationCode> GetPasswordConfirmationCodeByUserIdAsync(int id);
    }
}
