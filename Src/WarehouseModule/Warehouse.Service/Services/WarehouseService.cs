using AutoMapper;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class WarehouseService(
    IAdvancedFilterBuilder filterBuilder,
    IWarehouseRepository warehouseRepository,
    ICompanyService companyService,
    IMapper mapper,
    IValidator<Domain.Entities.Warehouse> validator,
    IStringLocalizer<WarehouseResource> localizer

) : BaseServiceWithCompany<
        Domain.Entities.Warehouse,
        WarehouseDto,
        WarehouseListDto,
        WarehouseParameters,
        IWarehouseRepository,
        WarehouseResource
    >(
        filterBuilder,
        warehouseRepository,
        companyService,
        mapper,
        validator,
        localizer
    ),
    IWarehouseService
{
    protected override string LocalizerKey => "Warehouse";

    public override Task<ListResponseModel<WarehouseListDto>> GetAllAsync(
        Guid companyId,
        WarehouseParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        return GetAllAsync(
            companyId,
            parameters,
            includeQuery,
            ct,
            filterNodeDto
        );
    }

    public override Task<WarehouseDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    ) => GetByIdAsync(companyId, id, includeQuery, ct);

    private static IQueryable<Domain.Entities.Warehouse> includeQuery(
        IQueryable<Domain.Entities.Warehouse> q
    ) => q.Include(c => c.WarehouseType).Include(c => c.CompanyUnit);
}
