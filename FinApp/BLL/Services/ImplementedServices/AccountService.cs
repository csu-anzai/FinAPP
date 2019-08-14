using AutoMapper;
using BLL.Services.IServices;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;

namespace BLL.Services.ImplementedServices
{
    public class AccountService : IAccountService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAccountRepository accountRepository;

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork, IAccountRepository accountRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.accountRepository = accountRepository;
        }

        public async Task<AccountDTO> GetInfoById(int userId, int accountId)
        {
            return mapper.Map<Account, AccountDTO>(await accountRepository.FindAsyncAccountWithImgCurrency(p => p.UserId == userId && p.Id == accountId));
        }

        public async Task<Account> AddAccount(AccountAddDTO account)
        {
            Account exitedAccount = await accountRepository.SingleOrDefaultAsync(a => a.Name == account.Name && a.UserId == account.UserId);

            if (exitedAccount == null)
            {
                await accountRepository.AddAsync(mapper.Map<AccountAddDTO, Account>(account));

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
