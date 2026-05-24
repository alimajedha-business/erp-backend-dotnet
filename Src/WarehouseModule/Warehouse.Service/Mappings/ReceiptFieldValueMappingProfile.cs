using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptFieldValueMappingProfile : Profile
{
    public ReceiptFieldValueMappingProfile()
    {
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
        CreateMap<CreateReceiptFieldValueDto, ReceiptFieldValue>();

        CreateMap<ReceiptSourceOfSupply, ReceiptFieldValueReferenceDto>()
            .ForCtorParam(
                nameof(ReceiptFieldValueReferenceDto.Code),
                opt => opt.MapFrom(src => src.Code)
            )
            .ForCtorParam(
                nameof(ReceiptFieldValueReferenceDto.Title),
                opt => opt.MapFrom(src => src.Title)
            );
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
            ReceiptFieldDataType.Reference => fieldValue.ReferenceId,
            _ => null
        };
    }
}
