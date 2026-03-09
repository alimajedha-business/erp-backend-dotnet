using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.Services;

public interface IDepartmentService : IBaseServiceWithCompany<
   Department,
   DepartmentDto,
   DepartmentParameters,
   IDepartmentRepository,
   HCMResource
    >
{
    Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        bool NewStatus,
        CancellationToken ct
        );
}