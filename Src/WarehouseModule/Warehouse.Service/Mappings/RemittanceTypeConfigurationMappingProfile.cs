using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class RemittanceTypeConfigurationMappingProfile : Profile
{
    public RemittanceTypeConfigurationMappingProfile()
    {
        CreateMap<RemittanceTypeConfiguration, RemittanceTypeConfigurationDto>();

        CreateMap<RemittanceTypeConfiguration, RemittanceTypeConfigurationListDto>()
            .ForCtorParam(
                nameof(RemittanceTypeConfigurationListDto.RemittanceTypeTitle),
                opt => opt.MapFrom(src => src.RemittanceType.Title)
            )
            .ForCtorParam(
                nameof(RemittanceTypeConfigurationListDto.FieldConfigurationCount),
                opt => opt.MapFrom(src => src.FieldConfigurations.Count)
            );

        CreateMap<CreateRemittanceTypeConfigurationDto, RemittanceTypeConfiguration>();
    }
}
