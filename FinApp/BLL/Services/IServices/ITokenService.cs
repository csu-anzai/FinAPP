using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface ITokenService
    {
        Task<Token> UpdateAsync(User user, string refreshToken);
    }
}
