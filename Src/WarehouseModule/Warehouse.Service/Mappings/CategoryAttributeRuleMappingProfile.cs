using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class CategoryAttributeRuleMappingProfile : Profile
{
    public CategoryAttributeRuleMappingProfile()
    {
        CreateMap<CategoryAttributeRule, CategoryAttributeRuleDto>();
        CreateMap<CategoryAttributeRule, CategoryAttributeRuleSlimDto>();
        CreateMap<CategoryAttributeRule, CategoryAttributeRuleListDto>()
            .ForCtorParam(
                nameof(CategoryAttributeRuleListDto.CategoryTitle),
                opt => opt.MapFrom(src => src.Category.Title)
            )
            .ForCtorParam(
                nameof(CategoryAttributeRuleListDto.AttributeTitle),
                opt => opt.MapFrom(src => src.Attribute.Title)
            );
        CreateMap<CreateCategoryAttributeRuleDto, CategoryAttributeRule>();
        CreateMap<PatchCategoryAttributeRuleDto, CategoryAttributeRule>().ReverseMap();
    }
}
