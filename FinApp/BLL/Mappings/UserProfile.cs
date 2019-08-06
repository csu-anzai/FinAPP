using AutoMapper;
using DAL.DTOs;
using DAL.Entities;

namespace BLL.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
      //      CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
