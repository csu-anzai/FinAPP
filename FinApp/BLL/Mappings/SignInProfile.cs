using AutoMapper;
using DAL.DTOs;
using DAL.Entities;

namespace BLL.Mappings
{
    public class SignInProfile : Profile
    {
        public SignInProfile()
        {
            CreateMap<UserLoginDTO, User>();
        }
    }
}
