using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptFieldDefinitionMappingProfile : Profile
{
    public ReceiptFieldDefinitionMappingProfile()
    {
        CreateMap<ReceiptFieldDefinition, ReceiptFieldDefinitionDto>()
            .ForCtorParam(
                nameof(ReceiptFieldDefinitionDto.DataTypeDescription),
                opt => opt.MapFrom(src =>
                    ReceiptFieldDefinitionDto.GetDataTypeDescription(src.DataType)
                )
            )
            .ForCtorParam(
                nameof(ReceiptFieldDefinitionDto.AllowedPlacementDescription),
                opt => opt.MapFrom(src =>
                    ReceiptFieldDefinitionDto.GetPlacementDescription(src.AllowedPlacement)
                )
            );

        CreateMap<ReceiptFieldDefinition, ReceiptFieldDefinitionListDto>();
    }
}
