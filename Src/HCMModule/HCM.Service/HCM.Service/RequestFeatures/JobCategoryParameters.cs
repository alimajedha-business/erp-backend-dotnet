using NGErp.Base.Service.RequestFeatures;

namespace NGErp.HCM.Service.RequestFeatures;

public class JobCategoryParameters : RequestParameters
{
    public JobCategoryParameters()
    {
        OrderBy = "code";
    }
}