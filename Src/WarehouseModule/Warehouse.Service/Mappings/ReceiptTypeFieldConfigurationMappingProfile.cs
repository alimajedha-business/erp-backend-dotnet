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
                nameof(ReceiptTypeFieldConfigurationDto.ReceiptTypeId),
                opt => opt.MapFrom(src => src.ReceiptTypeConfiguration.ReceiptTypeId)
            )
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationDto.PlacementDescription),
                opt => opt.MapFrom(src =>
                    ReceiptTypeFieldConfigurationDto.GetPlacementDescription(src.Placement)
                )
            );

        CreateMap<ReceiptTypeFieldConfiguration, ReceiptTypeFieldConfigurationListDto>()
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationListDto.FieldDefinitionKey),
                opt => opt.MapFrom(src => src.FieldDefinition.Key)
            )
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationListDto.FieldDefinitionTitle),
                opt => opt.MapFrom(src => src.FieldDefinition.Title)
            )
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationListDto.PlacementDescription),
                opt => opt.MapFrom(src =>
                    ReceiptTypeFieldConfigurationDto.GetPlacementDescription(src.Placement)
                )
            );

        CreateMap<CreateReceiptTypeFieldConfigurationDto, ReceiptTypeFieldConfiguration>();
        CreateMap<PatchReceiptTypeFieldConfigurationDto, ReceiptTypeFieldConfiguration>()
            .ReverseMap();
    }
}
