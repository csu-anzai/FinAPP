using DAL.Context;
using DAL.Repositories.ImplementedRepositories;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private bool _disposed = false;
        private IImageRepository _imageRepository;

        #region properties

        public IAccountRepository AccountRepository { get; }
        public IAuthRepository AuthRepository { get; }
        public ICurrencyRepository CurrencyRepository { get; }
        public IExpenseCategoryRepository ExpenseCategoryRepository { get; }
        public IImageRepository ImageRepository
        {
            get
            {
                if (this._imageRepository == null)
                {
                    this._imageRepository = new ImageRepository(_context);
                }
                return this._imageRepository;
            }
        }
        public IIncomeCategoryRepository IncomeCategoryRepository { get; }
        public IPasswordConfirmationCodeRepository PasswordConfirmationCodeRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public ITokenRepository TokenRepository { get; }
        public IUserRepository UserRepository { get; }
        #endregion properties

        #region constructor
        public UnitOfWork(FinAppContext context, IAccountRepository accountRepository, IAuthRepository authRepository, ICurrencyRepository currencyRepository,
            IExpenseCategoryRepository expenseCategoryRepository, /*IImageRepository imageRepository,*/ IIncomeCategoryRepository incomeCategoryRepository,
             IPasswordConfirmationCodeRepository passwordConfirmationCodeRepository, IRoleRepository roleRepository,
                ITokenRepository tokenRepository, IUserRepository userRepository)

        {
            _context = context;
            AccountRepository = accountRepository;
            AuthRepository = authRepository;
            CurrencyRepository = currencyRepository;
            ExpenseCategoryRepository = expenseCategoryRepository;
            //ImageRepository = imageRepository;
            IncomeCategoryRepository = incomeCategoryRepository;
            PasswordConfirmationCodeRepository = passwordConfirmationCodeRepository;
            RoleRepository = roleRepository;
            TokenRepository = tokenRepository;
            UserRepository = userRepository;
        }
        #endregion constructor

        #region methods
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion methods
    }
}

