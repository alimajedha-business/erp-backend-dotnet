namespace NGErp.Warehouse.Service.RequestExamples;

using Swashbuckle.AspNetCore.Filters;

public class ReceiptFieldDefinitionAdvancedSearchExample : IExamplesProvider<object>
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
                    field = "key",
                    @operator = "contains",
                    value = "invoice"
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
