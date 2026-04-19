using AutoMapper;
using AutoMapper.QueryableExtensions;

using DocumentFormat.OpenXml.Vml.Office;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.Services;

public class EmploymentGroupService(
    IEmploymentGroupRepository employmentGroupRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder,
    ICompanyService companyService
    ) 
    //IEmploymentGroupService
{
    //private readonly string _key = "EmploymentGroup";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IEmploymentGroupRepository _employmentGroupRepository = employmentGroupRepository;
    private readonly ICompanyService _companyService = companyService;

    public async Task<ListResponseModel<DepartmentDto>> GetFilteredAsync(
    Guid companyId,
    DepartmentParameters parameters,
    FilterNodeDto? filterNodeDto = null,
    CancellationToken ct = default
)
    {
        var advancedFilters = _filterBuilder.Build<Department>(filterNodeDto);
        var query = _employmentGroupRepository.GetFiltered(companyId, advancedFilters);
        var res = await _employmentGroupRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<DepartmentDto>(
            results: _mapper.Map<IReadOnlyList<DepartmentDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }
    //async Task<EmploymentGroupDetailDto?> IEmploymentGroupService.GetByIdAsync(
    //     Guid companyId,
    //     Guid id,
    //     CancellationToken ct
    //     )
    //{
    //    await EnsureCompanyAsync(companyId, ct);
    //    var entity = await _employmentGroupRepository
    //   .Find(companyId, e => e.Id == id)
    //   .AsNoTracking()
    //   .ProjectTo<EmploymentGroupDetailDto>(_mapper.ConfigurationProvider)
    //   .FirstOrDefaultAsync(ct);

    //    return entity ?? throw new NotFoundException(_localizer[_key].Value);
    //}
}