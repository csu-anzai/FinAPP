using DAL.DTOs;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IIncomeCategoryService
    {
        Task<IncomeCategory> CreateIncomeCategoryAsync(IncomeCategory incomeCategory);
        Task DeleteIncomeCategoryAsync(IncomeCategory incomeCategory);
        Task<IncomeCategory> UpdateIncomeCategoryAsync(IncomeCategoryDTO incomeCategoryDTO);
        Task<IncomeCategory> GetIncomeCategoryAsync(int id);
        Task<IEnumerable<IncomeCategoryDTO>> GetAllIncomeCategoryAsync();

    }
}
