using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
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

    public Task<UnitOfMeasurementDto> CreateUnitOfMeasurementAsync(
        Guid companyId,
        CreateUnitOfMeasurementDto createDto,
        CancellationToken ct
    ) => CreateAsync(companyId, createDto, ct);

    public Task<ListResponseModel<UnitOfMeasurementDto>> GetAllUnitOfMeasurementsAsync(
            Guid companyId,
            UnitOfMeasurementParameters parameters,
            CancellationToken ct,
            FilterNodeDto? filterNodeDto = null
        ) => GetAllAsync(companyId, parameters, ct, filterNodeDto);

    public Task<UnitOfMeasurementDto> GetUnitOfMeasurementByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    ) => GetByIdAsync(companyId, id, ct);

    public Task<UnitOfMeasurementDto> PatchUnitOfMeasurementAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchUnitOfMeasurementDto> patchDocument,
        CancellationToken ct
    ) => PatchAsync(companyId, id, patchDocument, ct);

    public Task DeleteUnitOfMeasurementAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    ) => DeleteAsync(companyId, id, ct);
}
