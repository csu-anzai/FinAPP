using AutoMapper;
using DAL.DTOs;
using DAL.Entities;

namespace BLL.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap()
                .ForMember(dest => dest.AccountsDTO, act => act.MapFrom(src => src.Accounts));
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            CreateMap<IncomeCategoryDTO,IncomeCategory>().ReverseMap();
            CreateMap<IncomeDTO, Income>().ReverseMap()
               .ForMember(dest => dest.IncomeCategoryDTO, act => act.MapFrom(src => src.IncomeCategory))
               .ForMember(dest => dest.TransactionDTO, act => act.MapFrom(src=>src.Transaction));
        }
    }
}
