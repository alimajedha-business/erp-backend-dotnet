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
            HasNextLevel = true,
            ParentLocationId = null,
            LevelNo = 2,
            Length = 12,
            Width = 4,
            Height = 3,
            MaxMass = 2500,
            MaxVolume = 144,
            PreferredMassUnitId = new Guid("A4E51B4D-2069-4F7A-B7AC-17D883FD0321"),
            PreferredLengthUnitId = new Guid("2AD8D6D2-36B3-452A-B1BB-80D86ACAE6E2"),
            PreferredVolumeUnitId = new Guid("15C38183-F807-46B7-B3B1-7653D9D6360B")
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
                    HasNextLevel: true,
                    LevelNo: 2,
                    Length: 12,
                    Width: 4,
                    Height: 3,
                    MaxMass: 2500,
                    MaxVolume: 144,
                    PreferredMassUnit: null,
                    PreferredLengthUnit: null,
                    PreferredVolumeUnit: null,
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
        patchDoc.Replace(x => x.HasNextLevel, false);
        patchDoc.Replace(x => x.Length, 14);
        patchDoc.Replace(x => x.Width, 4);
        patchDoc.Replace(x => x.Height, 3);
        patchDoc.Replace(x => x.MaxMass, 3000);
        patchDoc.Replace(x => x.MaxVolume, 168);

        return patchDoc;
    }
}
