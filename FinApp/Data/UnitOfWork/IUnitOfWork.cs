using DAL.Repositories.IRepositories;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        IAuthRepository AuthRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }
        IExpenseCategoryRepository ExpenseCategoryRepository { get; }
        IImageRepository ImageRepository { get; }
        IIncomeCategoryRepository IncomeCategoryRepository { get; }
        IPasswordConfirmationCodeRepository PasswordConfirmationCodeRepository { get; }
        IRoleRepository RoleRepository { get; }
        ITokenRepository TokenRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> Complete();
    }
}