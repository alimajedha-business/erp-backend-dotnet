using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

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
            Title = "شیر کاله",
            TitleInEnglish = "Kale Milk",
            TechnicalNumber = "11-Aq985-0012",
            Barcode = "010011447140",
            Sku = "10040-14001-112-2020",
            PrimaryUnitOfMeasurementId = new Guid("94E9EB2E-AB06-4AC6-92A1-FAFA5C765D37"),
            ItemTypeId = new Guid("3F148EF5-BF43-4D43-8E3A-B25DECC7D092"),
            CategoryId = new Guid("B634A73B-49F9-4B92-95D9-4AAC55AFF72E"),
            SecondaryUnitOfMeasurementIds = [new Guid("50C174BF-F9D6-4347-B62E-78E97514C1CA")],
            AttributeIds = [
                new Guid("47F6DAAE-73C0-4D04-AA71-2629056ACB86"),
                new Guid("87D8C665-2D17-44A5-AD91-1543DCEF77C1")
            ],
            ItemWarehouses = [
                new CreateItemWarehouseDto {
                    WarehouseId = new Guid("47F6DAAE-73C0-4D04-AA71-2629056ACB86"),
                    ReorderPoint = 1000,
                    CriticalPoint = 500,
                    ReorderQuantity = 3000,
                    MaxStockLevel = 10000,
                    LocationIds = [
                        new Guid("B634A73B-49F9-4B92-95D9-4AAC55AFF72E"),
                        new Guid("B634A73B-49F9-4B92-95D9-4AAC55AFF72F"),
                        new Guid("B634A73B-49F9-4B92-95D9-4AAC55AFF72A"),
                    ]
                }
            ],
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

public class GetItemsListExample : IExamplesProvider<ListResponseModel<ItemListDto>>
{
    public ListResponseModel<ItemListDto> GetExamples()
    {
        return new ListResponseModel<ItemListDto>(
            results: [
                new ItemListDto(
                    Id: new Guid("11111111-2222-3333-4444-555555555555"),
                    Code: "100500",
                    Title: "شیر کاله",
                    TitleInEnglish: "Kale Milk",
                    TechnicalNumber: "11-Aq985-0012",
                    Sku: "10040-14001-112-2020",
                    Barcode: "010011447140",
                    PrimaryUnitOfMeasurementTitle: "بطری",
                    CategoryTitle: "شیر فرادما",
                    ItemTypeTitle: "مصرفی",
                    IsActive: true
                )
            ],
            totalCount: 1,
            requestParameters: new ItemParameters { Paginated = false }
        );
    }
}

public class ItemPatchExample :
    IExamplesProvider<JsonPatchDocument<PatchItemDto>>
{
    public JsonPatchDocument<PatchItemDto> GetExamples()
    {
        var patchDoc = new JsonPatchDocument<PatchItemDto>();
        patchDoc.Replace(x => x.Code, "100501");
        patchDoc.Replace(x => x.Title, "New Item Title");
        patchDoc.Replace(x => x.TitleInEnglish, "New Item Title in English");
        patchDoc.Replace(x => x.TechnicalNumber, "12-Aed85-301FX");
        patchDoc.Replace(x => x.Barcode, "01051044136071");
        patchDoc.Replace(x => x.IsActive, false);
        patchDoc.Replace(x => x.PrimaryUnitOfMeasurementId, new Guid("50c174bf-f9d6-4347-b62e-78e97514c1ca"));
        patchDoc.Replace(x => x.ItemTypeId, new Guid("3f148ef5-bf43-4d43-8e3a-b25decc7d092"));

        return patchDoc;
    }
}
