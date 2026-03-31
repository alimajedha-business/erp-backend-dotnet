using Microsoft.AspNetCore.JsonPatch;

using NGErp.Warehouse.Service.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.Service.RequestExamples;

public class CreateItemTypeExample :
    IExamplesProvider<CreateItemTypeDto>
{
    public CreateItemTypeDto GetExamples()
    {
        return new CreateItemTypeDto
        {
            Code = 96050,
            Title = "اموالی",
        };
    }
}

public class ItemTypeAdvancedSearchExample : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            type = "condition",
            field = "title",
            @operator = "contains",
            value = "ام"
        };
    }
}

public class ItemTypePatchExample :
    IExamplesProvider<JsonPatchDocument<PatchItemTypeDto>>
{
    public JsonPatchDocument<PatchItemTypeDto> GetExamples()
    {
        var patchDoc = new JsonPatchDocument<PatchItemTypeDto>();
        patchDoc.Replace(x => x.Code, 75101);
        patchDoc.Replace(x => x.Title, "New Item Type");

        return patchDoc;
    }
}
