using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    interface IService<TResult> where TResult : class
    {
        Task<IEnumerable<TResult>> ReadAsync();
        Task<TResult> ReadAsync(int id);
        Task<TResult> CreateAsync(TResult user);
        Task<TResult> UpdateAsync(TResult user);
        Task<TResult> DeleteAsync(int id);
    }
}
