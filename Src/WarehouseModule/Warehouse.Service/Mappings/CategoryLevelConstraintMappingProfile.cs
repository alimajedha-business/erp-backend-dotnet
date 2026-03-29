using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class CategoryLevelConstraintMappingProfile : Profile
{
    public CategoryLevelConstraintMappingProfile()
    {
        CreateMap<CategoryLevelConstraint, CategoryLevelConstraintDto>();
        CreateMap<CreateCategoryLevelConstraintDto, CategoryLevelConstraint>();
        CreateMap<PatchCategoryLevelConstraintDto, CategoryLevelConstraint>().ReverseMap();
    }
}
