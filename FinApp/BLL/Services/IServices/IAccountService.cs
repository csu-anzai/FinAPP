using BLL.DTOs;
using BLL.Models.ViewModels;
using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IAccountService
    {
        Task<AccountDTO> GetInfoById(int userId, int accountId);

        Task<Account> AddAccount(AccountAddModel account);
    }
}
