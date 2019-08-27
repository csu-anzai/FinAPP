using AutoMapper;
using BLL.Models.ViewModels;
using BLL.DTOs;
using BLL.Services.IServices;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class AccountService : IAccountService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<AccountDTO> GetInfoById(int userId, int accountId)
        {
            return mapper.Map<Account, AccountDTO>(await  unitOfWork.AccountRepository.FindAsyncAccountWithImgCurrency(p => p.UserId == userId && p.Id == accountId));
        }

        public async Task<Account> AddAccount(AccountAddModel account)
        {
            Account exitedAccount = await unitOfWork.AccountRepository.SingleOrDefaultAsync(a => a.Name == account.Name && a.UserId == account.UserId);

            if (exitedAccount == null)
            {
                await unitOfWork.AccountRepository.AddAsync(mapper.Map<AccountAddModel, Account>(account));

                await unitOfWork.Complete();

                return null;
            }
            else
            {
                return exitedAccount;
            }
        }
    }
}
