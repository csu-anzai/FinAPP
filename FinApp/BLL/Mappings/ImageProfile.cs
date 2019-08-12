using AutoMapper;
using DAL.DTOs;
using DAL.Entities;

namespace BLL.Mappings
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ImageDTO, Image>().ReverseMap();
        }
    }
}
