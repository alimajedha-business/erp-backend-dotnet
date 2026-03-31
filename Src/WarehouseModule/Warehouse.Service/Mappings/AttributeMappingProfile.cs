using AutoMapper;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class AttributeMappingProfile : Profile
{
    public AttributeMappingProfile()
    {
        CreateMap<Domain.Entities.Attribute, AttributeDto>()
            .ForCtorParam(
                nameof(AttributeDto.DataTypeDescription),
                opt => opt.MapFrom(src => AttributeDto.GetDataTypeDescription(src.DataType))
            )
            .ForCtorParam(
                nameof(AttributeDto.AttributeEntityDescription),
                opt => opt.MapFrom(src => AttributeDto.GetEntityDescription(src.AttributeEntity))
            );
        CreateMap<Domain.Entities.Attribute, AttributeSlimDto>();
        CreateMap<CreateAttributeDto, Domain.Entities.Attribute>();
        CreateMap<PatchAttributeDto, Domain.Entities.Attribute>().ReverseMap();
    }
}
