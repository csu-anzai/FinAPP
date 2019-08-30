using BLL.Security;
using BLL.Services.ImplementedServices;
using BLL.Services.IServices;
using DAL.Context;
using DAL.Repositories.ImplementedRepositories;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using FinApp.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinApp.Extensions
{
    public static class ServicesConfiguration
    {
        public static void ConfigureDataAccessServices(this IServiceCollection services)
        {
            // DAL
            services.AddScoped<DbContext, FinAppContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IExpenseCategoryRepository, ExpenseRepository>();
            services.AddScoped<IIncomeCategoryRepository, IncomeRepository>();
            services.AddScoped<IPasswordConfirmationCodeRepository, PasswordConfirmationCodeRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();

            // BLL
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
            services.AddScoped<IIncomeCategoryService, IncomeCategoryService>();
            services.AddScoped<IPasswordConfirmationCodeService, PasswordConfirmationCodeService>();
            services.AddScoped<IEmailConfirmationService, EmailConfirmationService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUploadService, UploadService>();

            services.AddScoped<AuthorizeAttribute>();
            services.AddScoped<AuthorizeAttribute>();

            services.AddTransient<IPassHasher, PassHasher>();

            services.AddSingleton<IEmailSenderService, EmailSenderService>();
        }
    }
}
