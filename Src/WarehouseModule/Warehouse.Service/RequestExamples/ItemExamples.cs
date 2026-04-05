using Microsoft.AspNetCore.JsonPatch;

using NGErp.Warehouse.Service.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.Service.RequestExamples;

public class CreateItemExample :
    IExamplesProvider<CreateItemDto>
{
    public CreateItemDto GetExamples()
    {
        return new CreateItemDto
        {
            Code = "100500",
            Title = "Kale Milk",
            Sku = "10040-14001-112-2020",
            IsActive = true,
        };
    }
}

public class ItemAdvancedSearchExample : IExamplesProvider<object>
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
                    field = "code",
                    @operator = "contains",
                    value = "-0101-"
                },
                new
                {
                    type = "group",
                    op = "and",
                    children = new object[]
                    {
                        new
                        {
                            type = "condition",
                            field = "title",
                            @operator = "contains",
                            value = "uht"
                        },
                        new
                        {
                            type = "condition",
                            field = "code",
                            @operator = "startsWith",
                            value = "10"
                        }
                    }
                }
            }
        };
    }
}

public class ItemPatchExample :
    IExamplesProvider<JsonPatchDocument<PatchItemDto>>
{
    public JsonPatchDocument<PatchItemDto> GetExamples()
    {
        var patchDoc = new JsonPatchDocument<PatchItemDto>();
        patchDoc.Replace(x => x.Code, "100501");
        patchDoc.Replace(x => x.Title, "New Category Title");
        patchDoc.Replace(x => x.IsActive, false);

        return patchDoc;
    }
}
