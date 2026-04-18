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

public interface IEmploymentGroupService : IBaseServiceWithCompany<
   EmploymentGroup,
   EmploymentGroupDto,
   EmploymentGroupParameters,
   IEmploymentGroupRepository,
   HCMResource
    >
{
    new Task<EmploymentGroupDetailDto?> GetByIdAsync(
      Guid companyId,
      Guid id,
      CancellationToken ct
  );

    Task<EmploymentGroupDetailDto> CreateAsync(
        Guid companyId,
        CreateEmploymentGroupDto createDto,
        CancellationToken ct
        );

    Task<EmploymentGroupDetailDto> UpdateAsync(
        Guid companyId,
        Guid id,
        UpdateEmploymentGroupDto updateDto,
        CancellationToken ct);
}