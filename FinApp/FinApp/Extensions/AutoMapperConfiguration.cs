using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace FinApp.Extensions
{
    public static class AutoMapperConfiguration
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(c =>
            {
                c.CreateMap<AccountDTO, Account>().ReverseMap()
                   .ForMember(dest => dest.Incomes, act => act.MapFrom(src => src.Incomes));

                c.CreateMap<AccountAddDTO, Account>();

                c.CreateMap<CurrencyDTO, Currency>().ReverseMap();

                c.CreateMap<ImageDTO, Image>().ReverseMap();

                c.CreateMap<UserLoginDTO, User>();

                c.CreateMap<UserRegistrationDTO, User>();

                c.CreateMap<Payload, UserRegistrationDTO>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GivenName))
                    .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.FamilyName));

                c.CreateMap<UserDTO, User>().ReverseMap()
                    .ForMember(dest => dest.Accounts, act => act.MapFrom(src => src.Accounts));

                c.CreateMap<Transaction, TransactionDTO>().ReverseMap();

                c.CreateMap<IncomeCategoryDTO, IncomeCategory>().ReverseMap()
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));
                c.CreateMap<ExpenseCategoryDTO, ExpenseCategory>().ReverseMap();

                c.CreateMap<IncomeDTO, Income>().ReverseMap()
                   .ForMember(dest => dest.IncomeCategory, act => act.MapFrom(src => src.IncomeCategory))
                   .ForMember(dest => dest.Transaction, act => act.MapFrom(src => src.Transaction));

            }).CreateMapper());
        }
    }
}
