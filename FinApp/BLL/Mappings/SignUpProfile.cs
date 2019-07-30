using AutoMapper;
using DAL.DTOs;
using DAL.Entities;

namespace BLL.Mappings
{
    public class SignUpProfile : Profile
    {
        public SignUpProfile()
        {
            CreateMap<UserRegistrationDTO, User>();
        }
    }
}
