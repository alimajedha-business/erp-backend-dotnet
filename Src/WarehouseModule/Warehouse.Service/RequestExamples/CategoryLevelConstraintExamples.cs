using Microsoft.AspNetCore.JsonPatch;

using NGErp.Warehouse.Service.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.Service.RequestExamples;

public class CategoryLevelConstraintCreateRequestExample :
    IExamplesProvider<CreateCategoryLevelConstraintDto>
{
    public CreateCategoryLevelConstraintDto GetExamples()
    {
        return new CreateCategoryLevelConstraintDto
        {
            LevelNo = 3,
            CodeLength = 4,
        };
    }
}

public class CategoryLevelConstraintPatchRequestExample :
    IExamplesProvider<JsonPatchDocument<PatchCategoryLevelConstraintDto>>
{
    public JsonPatchDocument<PatchCategoryLevelConstraintDto> GetExamples()
    {
        var patchDoc = new JsonPatchDocument<PatchCategoryLevelConstraintDto>();
        patchDoc.Replace(x => x.CodeLength, 5);

        return patchDoc;
    }
}
