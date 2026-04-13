using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class UnitOfMeasurementService(
    IAdvancedFilterBuilder filterBuilder,
    IUnitOfMeasurementRepository unitOfMeasurementRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        UnitOfMeasurement,
        UnitOfMeasurementDto,
        UnitOfMeasurementListDto,
        UnitOfMeasurementParameters,
        IUnitOfMeasurementRepository,
        WarehouseResource
    >(
        filterBuilder,
        unitOfMeasurementRepository,
        mapper,
        localizer
    ),
    IUnitOfMeasurementService
{
    protected override string LocalizerKey => "UnitOfMeasurement";

    public override Task<UnitOfMeasurementDto> GetByIdAsync(
        Guid id,
        CancellationToken ct
    ) => GetByIdAsync(id, includeQuery, ct);

    public Task<int> GetNextCode(CancellationToken ct)
    {
        return _repo.GetNextCodeAsync(ct);
    }

    private static IQueryable<UnitOfMeasurement> includeQuery(
        IQueryable<UnitOfMeasurement> q
    ) => q.Include(c => c.MeasurementDimension);
}
