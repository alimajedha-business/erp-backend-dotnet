using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptTypeConfigurationMappingProfile : Profile
{
    public ReceiptTypeConfigurationMappingProfile()
    {
        CreateMap<ReceiptTypeConfiguration, ReceiptTypeConfigurationDto>();

        CreateMap<ReceiptTypeConfiguration, ReceiptTypeConfigurationListDto>()
            .ForCtorParam(
                nameof(ReceiptTypeConfigurationListDto.ReceiptTypeTitle),
                opt => opt.MapFrom(src => src.ReceiptType.Title)
            )
            .ForCtorParam(
                nameof(ReceiptTypeConfigurationListDto.FieldConfigurationCount),
                opt => opt.MapFrom(src => src.FieldConfigurations.Count)
            );

        CreateMap<CreateReceiptTypeConfigurationDto, ReceiptTypeConfiguration>();
    }
}
