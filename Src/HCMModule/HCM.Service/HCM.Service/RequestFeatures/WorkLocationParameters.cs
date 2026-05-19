using NGErp.Base.Service.RequestFeatures;

namespace NGErp.HCM.Service.RequestFeatures;

public class WorkLocationParameters : RequestParameters
{
    public Guid? ParentId { get; set; }
}
