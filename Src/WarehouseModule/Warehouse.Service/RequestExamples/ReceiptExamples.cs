using NGErp.Warehouse.Service.DTOs;

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
                    UnitOfMeasurementId = new Guid("943325AA-7E7D-4497-8919-B5C6B4F64C2C"),
                    Quantity = 12,
                    UnitPrice = 125000,
                    TotalPrice = 1500000,
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
