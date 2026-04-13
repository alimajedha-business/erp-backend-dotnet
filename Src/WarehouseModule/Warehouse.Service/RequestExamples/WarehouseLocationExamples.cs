using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.Service.RequestExamples;

public class WarehouseLocationCreateRequestExample :
    IExamplesProvider<CreateWarehouseLocationDto>
{
    public CreateWarehouseLocationDto GetExamples()
    {
        return new CreateWarehouseLocationDto
        {
            Code = 10040,
            Title = "راهرو اصلی",
            CanStoreItem = false,
            ParentLocationId = null,
            LevelNo = 0,
        };
    }
}

public class WarehouseLocationGetListResponseExample :
    IExamplesProvider<ListResponseModel<WarehouseLocationListDto>>
{
    public ListResponseModel<WarehouseLocationListDto> GetExamples()
    {
        return new ListResponseModel<WarehouseLocationListDto>(
            results: [
                new WarehouseLocationListDto(
                    Id: new Guid("11111111-2222-3333-4444-555555555555"),
                    Code: 10040,
                    Title: "راهرو اصلی",
                    CanStoreItem: true,
                    LevelNo: 2,
                    WarehouseTitle: "انبار مواد اولیه"
                )
            ],
            totalCount: 1,
            requestParameters: new ItemParameters { Paginated = false }
        );
    }
}

public class WarehouseLocationAdvancedSearchRequestExample : IExamplesProvider<object>
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
                    field = "canStoreItem",
                    @operator = "eq",
                    value = true
                },
                new
                {
                    type = "condition",
                    field = "parentLocationId",
                    @operator = "eq",
                    value = new Guid("3DC46175-6A49-40E4-928E-071ACE449F60")
                }
            }
        };
    }
}

public class WarehouseLocationPatchRequestExample :
    IExamplesProvider<JsonPatchDocument<PatchWarehouseLocationDto>>
{
    public JsonPatchDocument<PatchWarehouseLocationDto> GetExamples()
    {
        var patchDoc = new JsonPatchDocument<PatchWarehouseLocationDto>();
        patchDoc.Replace(x => x.Code, 10041);
        patchDoc.Replace(x => x.Title, "راهرو اصلی - ویرایش");
        patchDoc.Replace(x => x.CanStoreItem, true);

        return patchDoc;
    }
}