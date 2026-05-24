using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class FeatureSettingsMappingProfile : Profile
{
    public FeatureSettingsMappingProfile()
    {
        CreateMap<FeatureSettings, FeatureSettingsDto>()
            .ForCtorParam(
                nameof(FeatureSettingsDto.WarehouseSerialRuleDescription),
                opt => opt.MapFrom(src => FeatureSettingsDto
                    .GetWarehouseSerialRuleDescription(src.WarehouseSerialRule)
                )
            );
        CreateMap<CreateFeatureSettingsDto, FeatureSettings>();
        CreateMap<PatchFeatureSettingsDto, FeatureSettings>().ReverseMap();
    }
}