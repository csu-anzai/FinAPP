using AutoMapper;
using BLL.DTOs;
using BLL.Models.ViewModels;
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
                   .ForMember(dest => dest.Incomes, act => act.MapFrom(src => src.Incomes))
                   .ForMember(dest=>dest.Expenses, act => act.MapFrom(src=>src.Expenses));

                c.CreateMap<AccountAddModel, Account>();

                c.CreateMap<CurrencyDTO, Currency>().ReverseMap();

                c.CreateMap<ImageDTO, Image>().ReverseMap();

                c.CreateMap<ImageViewModel, Image>().ReverseMap();

                c.CreateMap<LoginViewModel, User>();

                c.CreateMap<RegistrationViewModel, User>();

                c.CreateMap<Payload, RegistrationViewModel>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GivenName))
                    .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.FamilyName))
                    .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Picture));

                c.CreateMap<UserDTO, User>().ReverseMap()
                    .ForMember(dest => dest.Accounts, act => act.MapFrom(src => src.Accounts));
                //.ForMember(dest => dest.Avatar, act => act.MapFrom(src => src.Avatar));

                c.CreateMap<Transaction, TransactionDTO>().ReverseMap();

                c.CreateMap<IncomeCategoryDTO, IncomeCategory>().ReverseMap();
                c.CreateMap<ExpenseCategoryDTO, ExpenseCategory>().ReverseMap();
                c.CreateMap<IncomeCategoryWithImageDTO, IncomeCategory>().ReverseMap()
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));
                c.CreateMap<ExpenseCategoryWithImageDTO, ExpenseCategory>().ReverseMap()
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));


                c.CreateMap<IncomeDTO, Income>().ReverseMap()
                   .ForMember(dest => dest.IncomeCategory, act => act.MapFrom(src => src.IncomeCategory))
                   .ForMember(dest => dest.Transaction, act => act.MapFrom(src => src.Transaction));

                c.CreateMap<ExpenseDTO, Expense>().ReverseMap()
                    .ForMember(dest => dest.ExpenseCategory, act => act.MapFrom(src => src.ExpenseCategory))
                    .ForMember(dest => dest.Transaction, act => act.MapFrom(src => src.Transaction));

                c.CreateMap<ProfileDTO, User>().ReverseMap();

                c.CreateMap<TransactionViewModel, Transaction>();

                c.CreateMap<IncomeAddViewModel, Income>();

            }).CreateMapper());
        }
    }
}
