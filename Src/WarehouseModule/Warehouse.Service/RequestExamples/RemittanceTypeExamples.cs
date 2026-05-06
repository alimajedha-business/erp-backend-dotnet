using Microsoft.AspNetCore.JsonPatch;

using NGErp.Warehouse.Service.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.Service.RequestExamples;

public class CreateRemittanceTypeExample :
    IExamplesProvider<CreateRemittanceTypeDto>
{
    public CreateRemittanceTypeDto GetExamples()
    {
        return new CreateRemittanceTypeDto
        {
            Code = 96050,
            Title = "Warehouse Remittance",
            AddToStock = false
        };
    }
}

public class RemittanceTypeAdvancedSearchExample : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            type = "condition",
            field = "title",
            @operator = "contains",
            value = "Warehouse"
        };
    }
}

public class RemittanceTypePatchExample :
    IExamplesProvider<JsonPatchDocument<PatchRemittanceTypeDto>>
{
    public JsonPatchDocument<PatchRemittanceTypeDto> GetExamples()
    {
        var patchDoc = new JsonPatchDocument<PatchRemittanceTypeDto>();
        patchDoc.Replace(x => x.Code, 75101);
        patchDoc.Replace(x => x.Title, "New Remittance Type");
        patchDoc.Replace(x => x.AddToStock, true);

        return patchDoc;
    }
}
