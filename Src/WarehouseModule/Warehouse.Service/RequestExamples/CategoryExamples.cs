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
            IsLastLevel = false,
            CategoryPath = "/FOOD/DAIRY",
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

public class CategoryPatchExample : 
    IExamplesProvider<JsonPatchDocument<PatchCategoryDto>>
{
    public JsonPatchDocument<PatchCategoryDto> GetExamples()
    {
        var patchDoc = new JsonPatchDocument<PatchCategoryDto>();
        patchDoc.Replace(x => x.Title, "New Category Title");
        patchDoc.Replace(x => x.CategoryPath, "New Category Path");
        patchDoc.Replace(x => x.IsLastLevel, false);

        return patchDoc;
    }
}