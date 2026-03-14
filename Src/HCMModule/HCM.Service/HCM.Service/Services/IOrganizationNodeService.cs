using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;
using NGErp.HCM.Service.Repository.Contracts;

namespace NGErp.HCM.Service.Services;

public interface IOrganizationNodeService : IBaseServiceWithCompany<
    OrganizationNode,
    OrganizationNodeParameters,
    OrganizationNodeParameters,
    IOrganizationNodeRepository,
    HCMResource
    >
{
}