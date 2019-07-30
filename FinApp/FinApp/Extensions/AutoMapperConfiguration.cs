using AutoMapper;
using DAL.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace FinApp.Extensions
{
    public static class AutoMapperConfiguration
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(c =>
            {
                c.AddProfile(new SignUpProfile());
            }).CreateMapper());
        }
    }
}
