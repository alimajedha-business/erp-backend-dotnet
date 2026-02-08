using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Attribute, AttributeDto>();
        CreateMap<CreateAttributeDto, Domain.Entities.Attribute>();
        CreateMap<UpdateAttributeDto, Domain.Entities.Attribute>()
            .ForMember(d => d.CompanyId, opt => opt.Ignore());

        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>()
            .ForMember(d => d.CompanyId, opt => opt.Ignore());

        CreateMap<Item, ItemDto>();
        CreateMap<CreateItemDto, Item>();
        CreateMap<UpdateItemDto, Item>()
            .ForMember(d => d.CompanyId, opt => opt.Ignore());
    }
}
