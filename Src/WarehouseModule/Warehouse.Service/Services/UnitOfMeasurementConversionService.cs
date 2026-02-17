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

public class UnitOfMeasurementConversionService(
    IAdvancedFilterBuilder filterBuilder,
    IUnitOfMeasurementConversionRepository unitOfMeasurementConversionRepository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseServiceWithCompany<
        UnitOfMeasurementConversion,
        UnitOfMeasurementConversionDto,
        UnitOfMeasurementConversionListDto,
        UnitOfMeasurementConversionParameters,
        IUnitOfMeasurementConversionRepository,
        WarehouseResource
    >(
        filterBuilder,
        unitOfMeasurementConversionRepository,
        companyService,
        mapper,
        localizer
    ),
    IUnitOfMeasurementConversionService
{
    protected override string LocalizerKey => "UnitOfMeasurementConversion";

    public Task<UnitOfMeasurementConversionDto> CreateUnitOfMeasurementConversionAsync(
        Guid companyId,
        CreateUnitOfMeasurementConversionDto createDto,
        CancellationToken ct
    ) => CreateAsync(companyId, createDto, ct);

    public Task<
        ListResponseModel<UnitOfMeasurementConversionListDto>
    > GetAllUnitOfMeasurementConversionsAsync(
        Guid companyId,
        UnitOfMeasurementConversionParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    ) => GetAllAsync(companyId, parameters, ct, filterNodeDto);

    public Task<UnitOfMeasurementConversionDto> GetUnitOfMeasurementConversionByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    ) => GetByIdAsync(companyId, id, ct);

    public Task<UnitOfMeasurementConversionDto> PatchUnitOfMeasurementConversionAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchUnitOfMeasurementConversionDto> patchDocument,
        CancellationToken ct
    ) => PatchAsync(companyId, id, patchDocument, ct);

    public Task DeleteUnitOfMeasurementConversionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    ) => DeleteAsync(companyId, id, ct);
}