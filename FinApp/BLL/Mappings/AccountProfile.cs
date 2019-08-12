using AutoMapper;
using DAL.DTOs;
using DAL.Entities;

namespace BLL.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountDTO, Account>().ReverseMap();

            CreateMap<AccountAddDTO, Account>();
        }
    }
}
