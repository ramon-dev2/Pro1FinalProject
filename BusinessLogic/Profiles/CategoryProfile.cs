using AutoMapper;
using BusinessLogic.Dtos;
using Store.Entities;

namespace BusinessLogic.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
        }
    }
}
