using NGErp.Base.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestFeatures;

public class ItemParameters : RequestParameters
{
    public Guid? CategoryId { get; set; }
}
