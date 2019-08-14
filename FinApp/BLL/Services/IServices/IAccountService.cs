using BLL.DTOs;
using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IAccountService
    {
        Task<AccountDTO> GetInfoById(int userId, int accountId);

        Task<Account> AddAccount(AccountAddDTO account);
    }
}
