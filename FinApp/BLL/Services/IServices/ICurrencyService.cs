using DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyDTO>> GetAllAsync();
    }
}
