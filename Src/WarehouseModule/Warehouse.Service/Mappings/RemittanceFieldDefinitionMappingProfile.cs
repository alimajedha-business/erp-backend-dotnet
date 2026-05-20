using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class RemittanceFieldDefinitionMappingProfile : Profile
{
    public RemittanceFieldDefinitionMappingProfile()
    {
        CreateMap<RemittanceFieldDefinition, RemittanceFieldDefinitionDto>()
            .ForCtorParam(
                nameof(RemittanceFieldDefinitionDto.DataTypeDescription),
                opt => opt.MapFrom(src =>
                    RemittanceFieldDefinitionDto.GetDataTypeDescription(src.DataType)
                )
            )
            .ForCtorParam(
                nameof(RemittanceFieldDefinitionDto.AllowedPlacementDescription),
                opt => opt.MapFrom(src =>
                    RemittanceFieldDefinitionDto.GetPlacementDescription(src.AllowedPlacement)
                )
            );

        CreateMap<RemittanceFieldDefinition, RemittanceFieldDefinitionListDto>();
    }
}
