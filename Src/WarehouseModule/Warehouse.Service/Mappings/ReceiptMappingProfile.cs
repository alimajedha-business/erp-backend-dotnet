using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptMappingProfile : Profile
{
    public ReceiptMappingProfile()
    {
        CreateMap<Receipt, ReceiptListDto>()
            .ForCtorParam(
                nameof(ReceiptListDto.ReceiptTypeTitle),
                opt => opt.MapFrom(src => src.ReceiptType.Title)
            )
            .ForCtorParam(
                nameof(ReceiptListDto.ReceiptLineCount),
                opt => opt.MapFrom(src => src.ReceiptLines.Count)
            )
            .ForCtorParam(
                nameof(ReceiptListDto.ReceiptFieldValues),
                opt => opt.MapFrom(src => src.ReceiptFieldValues
                    .Where(e => e.ReceiptLineId == null))
            );

        CreateMap<Receipt, ReceiptDto>();
        CreateMap<ReceiptLine, ReceiptLineDto>()
            .ForCtorParam(
                nameof(ReceiptLineDto.Weight),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.Weight, src.PreferredMassUnit))
            )
            .ForCtorParam(
                nameof(ReceiptLineDto.Volume),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.Volume, src.PreferredVolumeUnit))
            );
        CreateMap<ReceiptLineAttributeValue, ReceiptLineAttributeValueDto>();
        CreateMap<ReceiptLineMeasurementValue, ReceiptLineMeasurementValueDto>();
        CreateMap<ReceiptFieldValue, ReceiptFieldValueDto>();
        CreateMap<ReceiptFieldValue, ReceiptHeaderFieldValueListDto>()
            .ForCtorParam(
                nameof(ReceiptHeaderFieldValueListDto.FieldDefinitionTitle),
                opt => opt.MapFrom(src => src.FieldDefinition.Title)
            )
            .ForCtorParam(
                nameof(ReceiptHeaderFieldValueListDto.FieldDefinitionKey),
                opt => opt.MapFrom(src => src.FieldDefinition.Key)
            )
            .ForCtorParam(
                nameof(ReceiptHeaderFieldValueListDto.DataType),
                opt => opt.MapFrom(src => src.FieldDefinition.DataType)
            )
            .ForCtorParam(
                nameof(ReceiptHeaderFieldValueListDto.DataTypeDescription),
                opt => opt.MapFrom(src =>
                    ReceiptFieldDefinitionDto.GetDataTypeDescription(src.FieldDefinition.DataType)
                )
            )
            .ForCtorParam(
                nameof(ReceiptHeaderFieldValueListDto.Value),
                opt => opt.MapFrom(src => GetReceiptFieldValue(src))
            );

        CreateMap<CreateReceiptDto, Receipt>()
            .ForMember(d => d.Number, o => o.MapFrom(s => s.Number!))
            .ForMember(d => d.ReceiptLines, o => o.Ignore())
            .ForMember(d => d.ReceiptFieldValues, o => o.Ignore());

        CreateMap<CreateReceiptLineDto, ReceiptLine>()
            .ForMember(d => d.ReceiptLineMeasurementValues, o => o.Ignore())
            .ForMember(d => d.ReceiptLineAttributeValues, o => o.Ignore())
            .ForMember(d => d.ReceiptFieldValues, o => o.Ignore());

        CreateMap<CreateReceiptLineAttributeValueDto, ReceiptLineAttributeValue>();
        CreateMap<CreateReceiptLineMeasurementValueDto, ReceiptLineMeasurementValue>();

        CreateMap<CreateReceiptFieldValueDto, ReceiptFieldValue>();
    }

    private static object? GetReceiptFieldValue(ReceiptFieldValue fieldValue)
    {
        return fieldValue.FieldDefinition.DataType switch
        {
            ReceiptFieldDataType.String => fieldValue.StringValue,
            ReceiptFieldDataType.Integer => fieldValue.IntegerValue,
            ReceiptFieldDataType.Decimal => fieldValue.DecimalValue,
            ReceiptFieldDataType.Date => fieldValue.DateValue,
            ReceiptFieldDataType.Boolean => fieldValue.BooleanValue,
            ReceiptFieldDataType.Guid => fieldValue.ReferenceId,
            _ => null
        };
    }
}
