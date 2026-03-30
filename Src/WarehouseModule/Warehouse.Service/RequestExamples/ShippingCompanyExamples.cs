using Microsoft.AspNetCore.JsonPatch;

using NGErp.Warehouse.Service.DTOs;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.Service.RequestExamples;

public class CreateShippingCompanyExample :
    IExamplesProvider<CreateShippingCompanyDto>
{
    public CreateShippingCompanyDto GetExamples()
    {
        return new CreateShippingCompanyDto
        {
            Code = 96050,
            Title = "Fast Shippers",
            ManagerFirstName = "Ali",
            ManagerLastName = "Majed",
            MobileNumber = "09131047113",
            PhoneNumber = "03136247296",
            Address = "Some St., Isfahan, Iran"
        };
    }
}

public class ShippingCompanyAdvancedSearchExample : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            type = "condition",
            field = "title",
            @operator = "contains",
            value = "Fa"
        };
    }
}

public class ShippingCompanyPatchExample :
    IExamplesProvider<JsonPatchDocument<PatchShippingCompanyDto>>
{
    public JsonPatchDocument<PatchShippingCompanyDto> GetExamples()
    {
        var patchDoc = new JsonPatchDocument<PatchShippingCompanyDto>();
        patchDoc.Replace(x => x.Code, 75101);
        patchDoc.Replace(x => x.Title, "New Shipping Company Title");
        patchDoc.Replace(x => x.ManagerFirstName, "New Manager First Name");
        patchDoc.Replace(x => x.ManagerLastName, "New Manager Last Name");
        patchDoc.Replace(x => x.MobileNumber, "09131111111");
        patchDoc.Replace(x => x.PhoneNumber, "03111111111");
        patchDoc.Replace(x => x.Address, "New Address, Isfahan, Iran");

        return patchDoc;
    }
}
