using BLL.Security;
using BLL.Security.Jwt;
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddTransient<JwtManager>();
            services.AddTransient<IPassHasher, PassHasher>();
        }
    }
}
