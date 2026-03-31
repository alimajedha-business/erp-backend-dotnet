using Microsoft.AspNetCore.JsonPatch;

using NGErp.Warehouse.Service.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.Service.RequestExamples;

public class WarehouseTypeCategoryExample :
    IExamplesProvider<CreateWarehouseTypeDto>
{
    public CreateWarehouseTypeDto GetExamples()
    {
        return new CreateWarehouseTypeDto
        {
            Code = 2580,
            Title = "Quarantine",
            IsActive = true,
        };
    }
}

public class WarehouseTypeAdvancedSearchExample : IExamplesProvider<object>
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
                    field = "title",
                    @operator = "startsWith",
                    value = "qua"
                },
                new
                {
                    type = "condition",
                    field = "isActive",
                    @operator = "eq",
                    value = true
                }
            }
        };
    }
}

public class WarehouseTypePatchExample :
    IExamplesProvider<JsonPatchDocument<PatchWarehouseTypeDto>>
{
    public JsonPatchDocument<PatchWarehouseTypeDto> GetExamples()
    {
        var patchDoc = new JsonPatchDocument<PatchWarehouseTypeDto>();
        patchDoc.Replace(x => x.Code, 2081);
        patchDoc.Replace(x => x.Title, "New Category Title");
        patchDoc.Replace(x => x.IsActive, false);

        return patchDoc;
    }
}