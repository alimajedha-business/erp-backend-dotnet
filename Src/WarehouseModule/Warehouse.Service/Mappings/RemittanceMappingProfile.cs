using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class RemittanceMappingProfile : Profile
{
    public RemittanceMappingProfile()
    {
        CreateMap<Remittance, RemittanceDto>();
        CreateMap<Remittance, RemittanceListDto>()
            .ForMember(d => d.RemittanceTypeTitle, o => o.MapFrom(s => s.RemittanceType.Title))
            .ForMember(d => d.RemittanceLineCount, o => o.MapFrom(s => s.RemittanceLines.Count));

        CreateMap<RemittanceLine, RemittanceLineDto>();
        CreateMap<RemittanceFieldValue, RemittanceFieldValueDto>();
        CreateMap<RemittanceLineAttributeValue, RemittanceLineAttributeValueDto>();
        CreateMap<RemittanceLineMeasurementValue, RemittanceLineMeasurementValueDto>();
    }
}
