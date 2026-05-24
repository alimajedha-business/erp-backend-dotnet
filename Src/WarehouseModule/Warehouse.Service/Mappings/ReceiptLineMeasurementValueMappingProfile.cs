using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptLineMeasurementValueMappingProfile : Profile
{
    public ReceiptLineMeasurementValueMappingProfile()
    {
        CreateMap<ReceiptLineMeasurementValue, ReceiptLineMeasurementValueDto>();
        CreateMap<CreateReceiptLineMeasurementValueDto, ReceiptLineMeasurementValue>();
    }
}
