using AutoMapper;
using DAL.DTOs;
using DAL.Entities;

namespace DAL.Mappings
{
    public class SignUpProfile : Profile
    {
        public SignUpProfile()
        {
            CreateMap<UserRegistrationDTO, User>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname, opts => opts.MapFrom(src => src.LastName));
        }
    }
}
