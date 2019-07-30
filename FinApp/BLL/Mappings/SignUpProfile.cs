using AutoMapper;
using DAL.DTOs;
using DAL.Entities;

namespace DAL.Mappings
{
    public class SignUpProfile : Profile
    {
        public SignUpProfile()
        {
            CreateMap<UserRegistrationDTO, User>();
        }
    }
}
