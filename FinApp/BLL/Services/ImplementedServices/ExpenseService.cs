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
    public class ExpenseService : IExpenseService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public async Task<ExpenseAddViewModel> AddExpenseAsync(ExpenseAddViewModel expense)
        {
            var account = await _unitOfWork.AccountRepository.GetAsync(expense.AccountId);

            if (account == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, "Account was not found");
            }

            if (account.Balance > expense.Transaction.Sum)
            {
                throw new ApiException(HttpStatusCode.Forbidden, "Account has not enougth money");
            }

            var expenseMapped = _mapper.Map<Expense>(expense);

            Transaction tra = expenseMapped.Transaction;
            await _unitOfWork.TransactionRepository.AddAsync(tra);

            expenseMapped.TransactionId = tra.Id;
            await _unitOfWork.ExpenseRepository.AddAsync(expenseMapped);

            account.Balance -= tra.Sum;

            await _unitOfWork.Complete();

            return expense;
        }

        public async Task<Account> Remove(int id)
        {
            var expense = await _unitOfWork.ExpenseRepository
                .GetOneWithTransactionAsync(e => e.Id == id);

            if (expense == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, "Expense was not found");
            }

            _unitOfWork.ExpenseRepository.Remove(expense);                               
            _unitOfWork.TransactionRepository.Remove(expense.Transaction);

            var account = await _unitOfWork.AccountRepository.GetAsync(expense.AccountId);
            account.Balance += expense.Transaction.Sum;
            
            await _unitOfWork.Complete();

            return account;
        }

        public async Task<ExpenseDTO> UpdateExpense(ExpenseUpdateViewModel expense)
        {
            var expenseToUpdate = await _unitOfWork.IncomeRepository
                .GetOneWithTransactionAsync(e => e.Id == expense.Id);

            if (expenseToUpdate == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, "Expense was not found");
            }

            var account = await _unitOfWork.AccountRepository.GetAsync(expenseToUpdate.AccountId);

            account.Balance -= expense.Transaction.Sum - expenseToUpdate.Transaction.Sum;

            _mapper.Map(expense, expenseToUpdate);

            await _unitOfWork.Complete();

            return _mapper.Map<ExpenseDTO>(expenseToUpdate);
        }

        public async Task<IEnumerable<ExpenseDTO>> GetExpensesWithDetailsAndConditionAsync(TransactionOptions options)
        {

            if (!(await IsBelongAccountToUser(options.UserId, options.AccountId)))
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Invalid data in model (userId or accountId)");
            }

            var incomes = await _unitOfWork.ExpenseRepository.GetAllWithDetailsAsync(options.AccountId, options.FirstDate, options.SecondDate);

            var expensesDTOs = incomes.Select(_mapper.Map<Expense, ExpenseDTO>);

            if (!expensesDTOs.Any())
            {
                throw new ApiException(HttpStatusCode.NoContent, $"There is no incomes in this range {options.FirstDate} || {options.SecondDate}");
            }

            return expensesDTOs;
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
