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

        private  IAccountRepository _accountRepository;
        private  IAuthRepository _authRepository;
        private  ICurrencyRepository _currencyRepository;
        private  IExpenseCategoryRepository _expenseRepository;
        private  IImageRepository _imageRepository;
        private  IIncomeCategoryRepository _incomeRepository;
        private  IPasswordConfirmationCodeRepository _passwordConfirmRepository;
        private  IRoleRepository _roleRepository;
        private  ITokenRepository _tokenRepository;
        private  IUserRepository _userRepository;

        #region properties

        public IAccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                    _accountRepository = new AccountRepository(_context);

                return _accountRepository;
            }
        }

        public IAuthRepository AuthRepository
        {
            get
            {
                if (_authRepository == null)
                    _authRepository = new AuthRepository(_context);

                return _authRepository;
            }
        }

        public ICurrencyRepository CurrencyRepository
        {
            get
            {
                if (_currencyRepository == null)
                    _currencyRepository = new CurrencyRepository(_context);

                return _currencyRepository;
            }
        }

        public IExpenseCategoryRepository ExpenseCategoryRepository
        {
            get
            {
                if (_expenseRepository == null)
                    _expenseRepository = new ExpenseRepository(_context);

                return _expenseRepository;
            }
        }

        public IImageRepository ImageRepository
        {
            get
            {
                if (_imageRepository == null)
                    _imageRepository = new ImageRepository(_context);

                return _imageRepository;
            }
        }

        public IIncomeCategoryRepository IncomeCategoryRepository
        {
            get
            {
                if (_incomeRepository == null)
                    _incomeRepository = new IncomeRepository(_context);

                return _incomeRepository;
            }
        }

        public IPasswordConfirmationCodeRepository PasswordConfirmationCodeRepository
        {
            get
            {
                if (_passwordConfirmRepository == null)
                    _passwordConfirmRepository = new PasswordConfirmationCodeRepository(_context);

                return _passwordConfirmRepository;
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                    _roleRepository = new RoleRepository(_context);

                return _roleRepository;
            }
        }

        public ITokenRepository TokenRepository
        {
            get
            {
                if (_tokenRepository == null)
                    _tokenRepository = new TokenRepository(_context);

                return _tokenRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);

                return _userRepository;
            }
        }
        #endregion properties

        #region constructor
        public UnitOfWork(FinAppContext context)
        {
            _context = context;
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

