using BLL.DTOs;
using BLL.Models.ViewModels;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IIncomeService
    {
        Task<IncomeAddViewModel> AddIncomeAsync(IncomeAddViewModel account);
        Task<IEnumerable<IncomeDTO>> GetIncomesWithDetailsAndConditionAsync(IncomeOptions options);
    }
}
