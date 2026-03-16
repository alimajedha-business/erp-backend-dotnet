using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class CategoryLevelConstraintService(
    IAdvancedFilterBuilder filterBuilder,
    ICategoryLevelConstraintRepository constraintRepository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseServiceWithCompany<
        CategoryLevelConstraint,
        CategoryLevelConstraintDto,
        CategoryLevelConstraintParameters,
        ICategoryLevelConstraintRepository,
        WarehouseResource
    >(
        filterBuilder,
        constraintRepository,
        companyService,
        mapper,
        localizer
    ),
    ICategoryLevelConstraintService
{
    protected override string LocalizerKey => "CategoryLevelConstraint";

    public async Task<CategoryLevelConstraintDto> GetByLevelNo(
        Guid companyId,
        int levelNo,
        CancellationToken ct
    )
    {
        var constraint = await _repo.GetByLevelNo(companyId, levelNo, ct);
        return _mapper.Map<CategoryLevelConstraintDto>(constraint);
    }
}
