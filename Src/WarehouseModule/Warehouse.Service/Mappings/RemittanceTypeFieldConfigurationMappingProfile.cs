using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class RemittanceTypeFieldConfigurationMappingProfile : Profile
{
    public RemittanceTypeFieldConfigurationMappingProfile()
    {
        CreateMap<RemittanceTypeFieldConfiguration, RemittanceTypeFieldConfigurationDto>()
            .ForCtorParam(
                nameof(RemittanceTypeFieldConfigurationDto.RemittanceTypeId),
                opt => opt.MapFrom(src => src.RemittanceTypeConfiguration.RemittanceTypeId)
            )
            .ForCtorParam(
                nameof(RemittanceTypeFieldConfigurationDto.PlacementDescription),
                opt => opt.MapFrom(src => RemittanceTypeFieldConfigurationDto.GetPlacementDescription(src.Placement))
            );

        CreateMap<RemittanceTypeFieldConfiguration, RemittanceTypeFieldConfigurationListDto>()
            .ForCtorParam(
                nameof(RemittanceTypeFieldConfigurationListDto.RemittanceTypeId),
                opt => opt.MapFrom(src => src.RemittanceTypeConfiguration.RemittanceTypeId)
            )
            .ForCtorParam(
                nameof(RemittanceTypeFieldConfigurationListDto.FieldDefinitionKey),
                opt => opt.MapFrom(src => src.FieldDefinition.Key)
            )
            .ForCtorParam(
                nameof(RemittanceTypeFieldConfigurationListDto.FieldDefinitionTitle),
                opt => opt.MapFrom(src => src.FieldDefinition.Title)
            )
            .ForCtorParam(
                nameof(RemittanceTypeFieldConfigurationListDto.PlacementDescription),
                opt => opt.MapFrom(src => RemittanceTypeFieldConfigurationDto.GetPlacementDescription(src.Placement))
            );

        CreateMap<CreateRemittanceTypeFieldConfigurationDto, RemittanceTypeFieldConfiguration>();
    }
}
