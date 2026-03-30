using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class AttributeEnumMappingProfile : Profile
{
    public AttributeEnumMappingProfile()
    {
        CreateMap<AttributeEnumValue, AttributeEnumValueDto>();
        CreateMap<AttributeEnumValue, AttributeEnumValueListDto>()
            .ForCtorParam(
                nameof(AttributeEnumValueListDto.AttributeTitle),
                opt => opt.MapFrom(src => src.Attribute.Title)
            );
        CreateMap<CreateAttributeEnumValueDto, AttributeEnumValue>();
        CreateMap<PatchAttributeEnumValueDto, AttributeEnumValue>().ReverseMap();
    }
}
