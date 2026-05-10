using Microsoft.AspNetCore.JsonPatch;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.Service.RequestExamples;

public class CreateReceiptTypeFieldConfigurationExample :
    IExamplesProvider<CreateReceiptTypeFieldConfigurationDto>
{
    public CreateReceiptTypeFieldConfigurationDto GetExamples()
    {
        return new CreateReceiptTypeFieldConfigurationDto
        {
            FieldDefinitionId = new Guid("F6E53C25-B517-45B3-9A1A-5E2C7C8B7F1E"),
            Exists = true,
            IsRequired = false,
            Placement = ReceiptConfiguredPlacement.Header
        };
    }
}

public class ReceiptTypeFieldConfigurationAdvancedSearchExample :
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
                    field = "exists",
                    @operator = "eq",
                    value = true
                },
                new
                {
                    type = "condition",
                    field = "isRequired",
                    @operator = "eq",
                    value = false
                }
            }
        };
    }
}

public class ReceiptTypeFieldConfigurationPatchExample :
    IExamplesProvider<JsonPatchDocument<PatchReceiptTypeFieldConfigurationDto>>
{
    public JsonPatchDocument<PatchReceiptTypeFieldConfigurationDto> GetExamples()
    {
        var patchDoc = new JsonPatchDocument<PatchReceiptTypeFieldConfigurationDto>();
        patchDoc.Replace(x => x.Exists, true);
        patchDoc.Replace(x => x.IsRequired, false);
        patchDoc.Replace(x => x.Placement, ReceiptConfiguredPlacement.Detail);

        return patchDoc;
    }
}
