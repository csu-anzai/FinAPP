using AutoMapper;
using BLL.Services.IServices;
using BLL.Models.ViewModels;
using DAL.Entities;
using DAL.UnitOfWork;
using System.Threading.Tasks;

namespace BLL.Services.ImplementedServices
{
    public class IncomeService : IIncomeService
    {

        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public IncomeService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IncomeAddViewModel> AddIncomeAsync(IncomeAddViewModel income)
        {
            var incomeMapped = mapper.Map<Income>(income);
            Transaction tra = incomeMapped.Transaction;
            unitOfWork.TransactionRepository.Add(tra);

            await unitOfWork.Complete();
            incomeMapped.TransactionId = tra.Id;
           await  unitOfWork.IncomeRepository.AddAsync(incomeMapped);
           // await unitOfWork.IncomeRepository.AddAsync(income);

            await unitOfWork.Complete();

            return income;
        }
    }
}
