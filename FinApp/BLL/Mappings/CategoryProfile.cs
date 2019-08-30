using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<ExpenseCategoryDTO, ExpenseCategory>().ReverseMap();
            CreateMap<IncomeCategoryDTO, IncomeCategory>().ReverseMap();
        }
    }
}
