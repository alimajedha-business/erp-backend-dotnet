using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategorySlimDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<PatchCategoryDto, Category>().ReverseMap();
    }
}
