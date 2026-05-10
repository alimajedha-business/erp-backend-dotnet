using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.Service.RequestExamples;

public class CreateReceiptTypeConfigurationExample :
    IExamplesProvider<CreateReceiptTypeConfigurationDto>
{
    public CreateReceiptTypeConfigurationDto GetExamples()
    {
        return new CreateReceiptTypeConfigurationDto
        {
            ReceiptTypeId = new Guid("8E79616C-27B1-495B-88D4-9D9D1B76F508"),
            FieldConfigurations =
            [
                new CreateReceiptTypeFieldConfigurationDto
                {
                    FieldDefinitionId = new Guid("F6E53C25-B517-45B3-9A1A-5E2C7C8B7F1E"),
                    Exists = true,
                    IsRequired = false,
                    Placement = ReceiptConfiguredPlacement.Header
                },
                new CreateReceiptTypeFieldConfigurationDto
                {
                    FieldDefinitionId = new Guid("A09A5FB5-1FE4-45DD-8DBD-3ED0746F1BDE"),
                    Exists = true,
                    IsRequired = true,
                    Placement = ReceiptConfiguredPlacement.Detail
                }
            ]
        };
    }
}

public class ReceiptTypeConfigurationAdvancedSearchExample :
    IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            type = "group",
            op = "and",
            children = new object[]
            {
                new
                {
                    type = "condition",
                    field = "receiptTypeId",
                    @operator = "eq",
                    value = "8E79616C-27B1-495B-88D4-9D9D1B76F508"
                }
            }
        };
    }
}
