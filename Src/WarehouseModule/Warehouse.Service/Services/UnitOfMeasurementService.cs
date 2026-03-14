using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class UnitOfMeasurementService(
    IAdvancedFilterBuilder filterBuilder,
    IUnitOfMeasurementRepository unitOfMeasurementRepository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseServiceWithCompany<
        UnitOfMeasurement,
        UnitOfMeasurementDto,
        UnitOfMeasurementListDto,
        UnitOfMeasurementParameters,
        IUnitOfMeasurementRepository,
        WarehouseResource
    >(
        filterBuilder,
        unitOfMeasurementRepository,
        companyService,
        mapper,
        localizer
    ),
    IUnitOfMeasurementService
{
    protected override string LocalizerKey => "UnitOfMeasurement";

    public override Task<UnitOfMeasurementDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    ) => GetByIdAsync(companyId, id, includeQuery, ct);

    private static IQueryable<UnitOfMeasurement> includeQuery(
        IQueryable<UnitOfMeasurement> q
    ) => q.Include(c => c.MeasurementDimension);
}
