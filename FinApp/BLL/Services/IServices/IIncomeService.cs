using BLL.Models.ViewModels;
using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IIncomeService
    {
        Task<IncomeAddViewModel> AddIncomeAsync(IncomeAddViewModel account);
    }
}
