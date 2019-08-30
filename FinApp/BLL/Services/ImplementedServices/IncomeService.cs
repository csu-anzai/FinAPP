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
            var incomeMapped = _mapper.Map<Income>(income);
            Transaction tra = incomeMapped.Transaction;
            _unitOfWork.TransactionRepository.Add(tra);

            await _unitOfWork.Complete();
            incomeMapped.TransactionId = tra.Id;
            await  _unitOfWork.IncomeRepository.AddAsync(incomeMapped);
           // await unitOfWork.IncomeRepository.AddAsync(income);

            await _unitOfWork.Complete();

            return income;
        }

        public async Task<IEnumerable<IncomeDTO>> GetIncomesWithDetailsAndConditionAsync(IncomeOptions options)
        {

            if (!(await IsBelongAccountToUser(options.UserId, options.AccountId)))
                throw new ApiException(HttpStatusCode.BadRequest, "Invalid data in model (userId or accountId)");

            var incomes = (await _unitOfWork.IncomeRepository.GetAllWithDetailsAsync(options.AccountId))
                .Where(i => i.Transaction.Date <= options.SecondDate && i.Transaction.Date >= options.FirstDate)
                .OrderByDescending(i => i.Transaction.Date);

            var incomesDTOs = incomes.Select(_mapper.Map<Income, IncomeDTO>);

            if (!incomesDTOs.Any())
                throw new ApiException(HttpStatusCode.NoContent, $"There is no incomes in this range {options.FirstDate} || {options.SecondDate}");

            return incomesDTOs;
        }

        private async Task<bool> IsBelongAccountToUser(int userId, int accountId)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(userId);
            var account = await _unitOfWork.AccountRepository.GetAsync(accountId);

            if (user == null || account == null)
                return false;

            foreach (var userAccount in user.Accounts)
            {
                if (userAccount.Id == account.Id)
                    return true;
            }

            return false;
        }
    }
}
