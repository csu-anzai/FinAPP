using BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyDTO>> GetAllAsync();
    }
}
