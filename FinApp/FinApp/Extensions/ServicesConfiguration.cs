using BLL.Security;
using BLL.Services.ImplementedServices;
using BLL.Services.IServices;
using DAL.Context;
using DAL.Repositories.ImplementedRepositories;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinApp.Extensions
{
    public static class ServicesConfiguration
    {
        public static void ConfigureDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<DbContext, FinAppContext>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddTransient<IPassHasher, PassHasher>();
        }
    }
}
