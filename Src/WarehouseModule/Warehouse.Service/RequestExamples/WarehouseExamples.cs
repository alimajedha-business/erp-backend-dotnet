using Microsoft.AspNetCore.JsonPatch;

using NGErp.Warehouse.Service.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.Service.RequestExamples;

public class CreateWarehouseExample : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Code = 10040,
            Title = "Warehouse No. 1010",
            WarehouseTypeId = new Guid("3DC46175-6A49-40E4-928E-071ACE449F60"),
            CompanyUnitId = new Guid("ACD2FE45-BD2A-4B6C-A731-0CA3A678BBAE"),
            IsActive = true,
            MaxMonetaryValue = 120000,
            WarehouseSlaveAccountCompanyId = new Guid("9D6A7AAE-BFD1-4894-9B6D-20AC1EEDFA6B"),
            ExportSaleAccountMasterValue = "100",
            ExportSaleAccountSlaveValue = "25",
            ExportSaleAccountDetailed1Value = "سطح 2",
            ExportSaleAccountDetailed2Value = "10",
        };
    }
}

public class WarehouseAdvancedSearchExample : IExamplesProvider<object>
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
                    @operator = "gt",
                    value = 100
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
                            field = "warehouseTypeId",
                            @operator = "eq",
                            value = new Guid("3DC46175-6A49-40E4-928E-071ACE449F60")
                        },
                        new
                        {
                            type = "condition",
                            field = "maxMonetaryValue",
                            @operator = "gt",
                            value = 10000
                        }
                    }
                }
            }
        };
    }
}

public class WarehousePatchExample :
    IExamplesProvider<JsonPatchDocument<PatchWarehouseDto>>
{
    public JsonPatchDocument<PatchWarehouseDto> GetExamples()
    {
        var patchDoc = new JsonPatchDocument<PatchWarehouseDto>();
        patchDoc.Replace(x => x.Code, 1001);
        patchDoc.Replace(x => x.Title, "New Category Title");
        patchDoc.Replace(x => x.WarehouseTypeId, new Guid("3DC46175-6A49-40E4-928E-071ACE449F60"));
        patchDoc.Replace(x => x.CompanyUnitId, new Guid("ACD2FE45-BD2A-4B6C-A731-0CA3A678BBAE"));
        patchDoc.Replace(x => x.IsActive, false);

        return patchDoc;
    }
}