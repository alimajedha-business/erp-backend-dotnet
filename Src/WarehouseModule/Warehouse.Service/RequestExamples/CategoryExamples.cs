using Microsoft.AspNetCore.JsonPatch;

using NGErp.Warehouse.Service.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.Service.RequestExamples;

public class CreateCategoryExample :
    IExamplesProvider<CreateCategoryDto>
{
    public CreateCategoryDto GetExamples()
    {
        return new CreateCategoryDto
        {
            Code = "100500",
            Title = "Dairy",
            LevelNo = 3,
            HasNextLevel = false,
            ParentCategoryId = new Guid("B1EEAA38-0D79-4520-8C5A-DDEB63DE1351")
        };
    }
}

public class CategoryAdvancedSearchExample : IExamplesProvider<object>
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
                            field = "hasNextLevel",
                            @operator = "eq",
                            value = true
                        }
                    }
                }
            }
        };
    }
}

public class CategoryPatchExample : 
    IExamplesProvider<JsonPatchDocument<PatchCategoryDto>>
{
    public JsonPatchDocument<PatchCategoryDto> GetExamples()
    {
        var patchDoc = new JsonPatchDocument<PatchCategoryDto>();
        patchDoc.Replace(x => x.Title, "New Category Title");
        patchDoc.Replace(x => x.HasNextLevel, false);

        return patchDoc;
    }
}