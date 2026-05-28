using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptTypeFieldConfigurationMappingProfile : Profile
{
    public ReceiptTypeFieldConfigurationMappingProfile()
    {
        CreateMap<ReceiptTypeFieldConfiguration, ReceiptTypeFieldConfigurationDto>()
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationDto.FieldDefinitionKey),
                opt => opt.MapFrom(src => src.FieldDefinition.Key)
            )
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationDto.FieldDefinitionTitle),
                opt => opt.MapFrom(src => src.FieldDefinition.Title)
            )
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationDto.PlacementDescription),
                opt => opt.MapFrom(src =>
                    ReceiptTypeFieldConfigurationDto.GetPlacementDescription(src.Placement)
                )
            )
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationDto.ReferenceEntityType),
                opt => opt.MapFrom(src => src.FieldDefinition.ReferenceEntityType)
            )
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationDto.FieldDataType),
                opt => opt.MapFrom(src => src.FieldDefinition.DataType)
            )
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationDto.FieldDataTypeDescription),
                opt => opt.MapFrom(src =>
                    ReceiptTypeFieldConfigurationDto.GetDataTypeDescription(src.FieldDefinition.DataType)
                )
            );

        CreateMap<CreateReceiptTypeFieldConfigurationDto, ReceiptTypeFieldConfiguration>();
        CreateMap<PatchReceiptTypeFieldConfigurationDto, ReceiptTypeFieldConfiguration>()
            .ReverseMap();
    }
}
