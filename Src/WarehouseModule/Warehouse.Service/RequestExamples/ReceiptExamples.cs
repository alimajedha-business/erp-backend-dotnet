using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.Service.RequestExamples;

public class CreateReceiptExample :
    IExamplesProvider<CreateReceiptDto>
{
    public CreateReceiptDto GetExamples()
    {
        return new CreateReceiptDto
        {
            Number = 14030001,
            ReceiptDate = new DateOnly(2026, 5, 9),
            ReceiptTypeId = new Guid("8E79616C-27B1-495B-88D4-9D9D1B76F508"),
            Description = "Initial stock receipt",
            ReceiptFieldValues =
            [
                new CreateReceiptFieldValueDto
                {
                    FieldDefinitionId = new Guid("F6E53C25-B517-45B3-9A1A-5E2C7C8B7F1E"),
                    StringValue = "PO-2026-0001"
                }
            ],
            ReceiptLines =
            [
                new CreateReceiptLineDto
                {
                    RowNumber = 1,
                    ItemId = new Guid("EA128520-C888-43FD-8B54-03BB1D277BD6"),
                    WarehouseLocationId = new Guid("B634A73B-49F9-4B92-95D9-4AAC55AFF72E"),
                    ReceiptLineMeasurementValues =
                    [
                        new CreateReceiptLineMeasurementValueDto
                        {
                            ItemUnitOfMeasurementId = new Guid("943325AA-7E7D-4497-8919-B5C6B4F64C2C"),
                            Quantity = 12
                        }
                    ],
                    UnitPrice = 125000,
                    TotalPrice = 1500000,
                    BatchNumber = "202256799",
                    SerialNumber = "65879000020012",
                    ExpiryDate = new DateTime(2026, 5, 29),
                    Weight = 10,
                    Volume = 100,
                    PreferredMassUnitId = new Guid(),
                    PreferredVolumeUnitId = new Guid(),
                    ReceiptLineAttributeValues =
                    [
                        new CreateReceiptLineAttributeValueDto
                        {
                            ItemAttributeId = new Guid(
                                "427ADDD0-1D3A-4B58-8BC3-B9D8EA67B173"
                            ),
                            StringValue = "BATCH-001"
                        }
                    ],
                    ReceiptFieldValues =
                    [
                        new CreateReceiptFieldValueDto
                        {
                            FieldDefinitionId = new Guid(
                                "A09A5FB5-1FE4-45DD-8DBD-3ED0746F1BDE"
                            ),
                            DateValue = new DateOnly(2027, 5, 9)
                        }
                    ]
                }
            ]
        };
    }
}

public class ReceiptsGetListExample : IExamplesProvider<ListResponseModel<ReceiptListDto>>
{
    public ListResponseModel<ReceiptListDto> GetExamples()
    {
        return new ListResponseModel<ReceiptListDto>(
            results: [
                new ReceiptListDto(
                    Id: new Guid("11111111-2222-3333-4444-555555555555"),
                    Number: 1000500,
                    ReceiptDate: new DateOnly(2026, 05, 20),
                    ReceiptTypeId: new Guid("11111111-2222-3333-4444-555555555555"),
                    Status: ReceiptStatus.Draft,
                    ReceiptTypeTitle: "رسید خرید داخلی",
                    Description: "توضیح نوع رسید خرید داخلی",
                    ReceiptLineCount: 2,
                    ReceiptFieldValues:
                    [
                        new ReceiptHeaderFieldValueListDto(
                            Id: new Guid("F6E53C25-B517-45B3-9A1A-5E2C7C8B7F1E"),
                            FieldDefinitionId: new Guid(
                                "A09A5FB5-1FE4-45DD-8DBD-3ED0746F1BDE"
                            ),
                            FieldDefinitionTitle: "هزینه حمل و نقل",
                            FieldDefinitionKey: "shipping_cost",
                            DataType: ReceiptFieldDataType.String,
                            DataTypeDescription: "Text",
                            Value: "PO-2026-0001"
                        )
                    ]
                )
            ],
            totalCount: 1,
            requestParameters: new ItemParameters { Paginated = false }
        );
    }
}

public class ReceiptAdvancedSearchExample : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            type = "group",
            op = "or",
            children = new object[]
            {
                new
                {
                    type = "condition",
                    field = "receiptDate",
                    @operator = "lt",
                    value = "2026-10-11"
                },
                new
                {
                    type = "group",
                    op = "or",
                    children = new object[]
                    {
                        new
                        {
                            type = "condition",
                            field = "number",
                            @operator = "eq",
                            value = "1001",
                        },
                        new
                        {
                            type = "condition",
                            field = "receiptTypeId",
                            @operator = "eq",
                            value = new Guid()
                        }
                    }
                }
            }
        };
    }
}
