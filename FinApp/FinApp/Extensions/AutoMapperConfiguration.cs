using AutoMapper;
using BLL.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace FinApp.Extensions
{
    public static class AutoMapperConfiguration
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(c =>
            {
                c.AddProfile(new SignInProfile());
                c.AddProfile(new SignUpProfile());
                c.AddProfile(new UserProfile());
                c.AddProfile(new AccountProfile());
                c.AddProfile(new ImageProfile());
                c.AddProfile(new CurrencyProfile());
                c.AddProfile(new CategoryProfile());
            }).CreateMapper());
        }
    }
}
