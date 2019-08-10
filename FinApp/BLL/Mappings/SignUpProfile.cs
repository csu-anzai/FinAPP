using AutoMapper;
using DAL.DTOs;
using DAL.Entities;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace BLL.Mappings
{
    public class SignUpProfile : Profile
    {
        public SignUpProfile()
        {
            CreateMap<UserRegistrationDTO, User>();
            CreateMap<Payload, UserRegistrationDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GivenName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.FamilyName));
        }
    }
}
