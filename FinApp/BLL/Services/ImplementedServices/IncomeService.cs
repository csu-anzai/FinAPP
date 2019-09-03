using AutoMapper;
using BLL.Services.IServices;
using BLL.Models.ViewModels;
using DAL.Entities;
using DAL.UnitOfWork;
using System.Threading.Tasks;
using System.Collections.Generic;
using BLL.DTOs;
using System.Linq;
using BLL.Models.Exceptions;
using System.Net;

namespace BLL.Services.ImplementedServices
{
    public class IncomeService : IIncomeService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public IncomeService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IncomeAddViewModel> AddIncomeAsync(IncomeAddViewModel income)
        {
            var account = await _unitOfWork.AccountRepository.GetAsync(income.AccountId);

            if (account == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, "Account was not found");
            }

            var incomeMapped = _mapper.Map<Income>(income);

            Transaction tra = incomeMapped.Transaction;
            await _unitOfWork.TransactionRepository.AddAsync(tra);

            incomeMapped.TransactionId = tra.Id;

            await _unitOfWork.IncomeRepository.AddAsync(incomeMapped);

            account.Balance += tra.Sum;

            await _unitOfWork.Complete();

            return income;
        }

        public async Task<IncomeDTO> UpdateIncome(IncomeUpdateViewModel income)
        {
            var incomeToUpdate = await _unitOfWork.IncomeRepository
                .GetOneWithTransactionAsync(e => e.Id == income.Id);

            if (incomeToUpdate == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, "Income was not found");
            }

            var account = await _unitOfWork.AccountRepository.GetAsync(incomeToUpdate.AccountId);

            account.Balance += income.Transaction.Sum - incomeToUpdate.Transaction.Sum;

            _mapper.Map(income, incomeToUpdate);

            await _unitOfWork.Complete();

            return _mapper.Map<IncomeDTO>(incomeToUpdate);
        }

        public async Task<Account> Remove(int id)
        {
            var income = await _unitOfWork.IncomeRepository
                .GetOneWithTransactionAsync(e => e.Id == id);

            if (income == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, "Income was not found");
            }

            _unitOfWork.IncomeRepository.Remove(income);
            _unitOfWork.TransactionRepository.Remove(income.Transaction);

            var account = await _unitOfWork.AccountRepository.GetAsync(income.AccountId);
            account.Balance -= income.Transaction.Sum;
            
            await _unitOfWork.Complete();

            return account;
        }

        public async Task<IEnumerable<IncomeDTO>> GetIncomesWithDetailsAndConditionAsync(TransactionOptions options)
        {

            if (!(await IsBelongAccountToUser(options.UserId, options.AccountId)))
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Invalid data in model (userId or accountId)");
            }

            var incomes = await _unitOfWork.IncomeRepository.GetAllWithDetailsAsync(options.AccountId, options.FirstDate, options.SecondDate);

            var incomesDTOs = incomes.Select(_mapper.Map<Income, IncomeDTO>);

            if (!incomesDTOs.Any())
            {
                throw new ApiException(HttpStatusCode.NoContent, $"There is no incomes in this range {options.FirstDate} || {options.SecondDate}");
            }

            return incomesDTOs;
        }

        private async Task<bool> IsBelongAccountToUser(int userId, int accountId)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(userId);
            var account = await _unitOfWork.AccountRepository.GetAsync(accountId);

            if (user == null || account == null)
            {
                return false;
            }

            if (account.UserId != user.Id)
            {
                return false;
            }

            return true;
        }
    }
}
