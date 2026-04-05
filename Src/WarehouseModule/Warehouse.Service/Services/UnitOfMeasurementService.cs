using AutoMapper;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class UnitOfMeasurementService(
    IAdvancedFilterBuilder filterBuilder,
    IUnitOfMeasurementRepository unitOfMeasurementRepository,
    IMapper mapper,
    IValidator<UnitOfMeasurement> validator,
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
        validator,
        localizer
    ),
    IUnitOfMeasurementService
{
    protected override string LocalizerKey => "UnitOfMeasurement";

    public override Task<UnitOfMeasurementDto> GetByIdAsync(
        Guid id,
        CancellationToken ct
    ) => GetByIdAsync(id, includeQuery, ct);

    private static IQueryable<UnitOfMeasurement> includeQuery(
        IQueryable<UnitOfMeasurement> q
    ) => q.Include(c => c.MeasurementDimension);
}
